using Legislation.Data;
using Microsoft.EntityFrameworkCore;
using ReferendumEntity = Legislation.Data.ReferendumEntity.Referendum;

namespace Legislation.Api.Referendum;

public interface IReferendumRepository
{
    List<ReferendumEntity> GetMany();
    ReferendumEntity GetByID(int id);
    ReferendumEntity Create(CreateReferendumRequest req);
    ReferendumEntity Update(UpdateReferendumRequest req);
    bool Delete(int id);
}
public class ReferendumRepository(LegislationContext context) : IReferendumRepository
{
    public List<ReferendumEntity> GetMany() 
    {
        var referendums = context.Referendums
                            .AsNoTracking()
                            .Include(r => r.Laws);
        return [.. referendums];
    }

    public ReferendumEntity GetByID(int id) 
    {
        return context.Referendums
            .AsNoTracking()
            .Include(r => r.Laws)
            .Single();
    }

    ///TODO: add better collision handling
    public ReferendumEntity Create(CreateReferendumRequest req) 
    {
        var now = DateTime.UtcNow;
        var newEntity = new ReferendumEntity
        {
            Name = req.Name,
            Laws = req.Laws ?? [],
            CreatedAt = now,
            LastUpdated = now
        };

        var createdEntity = context.Referendums.Add(newEntity);
        context.SaveChanges();
        return createdEntity.Entity;
    }

    public ReferendumEntity Update(UpdateReferendumRequest req) 
    {
        var referendum = context.Referendums
                    .AsNoTracking()
                    .SingleOrDefault(r => r.ID == req.ReferendumID) ?? throw new ArgumentException("Invalid referendum ID");

        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            referendum.Name = req.Name;
        }
        referendum.Ended = req.Ended ?? false;
        referendum.LastUpdated = DateTime.UtcNow;

        var updatedEntity = context.Referendums.Update(referendum);
        context.SaveChanges();
        return updatedEntity.Entity;
    }

    public bool Delete(int id) 
    {
        var referendum = context.Referendums
                            .AsNoTracking()
                            .SingleOrDefault(r => r.ID == id);

        if (referendum is null)
        {
            return false;
        }

        context.Referendums.Remove(referendum);
        context.SaveChanges();
        return true;
    }
}
