using Legislation.Data;
using Microsoft.EntityFrameworkCore;
using LawEntity = Legislation.Data.LawEntity.Law;

namespace Legislation.Api.Law;

public interface ILawRepository 
{ 
    List<LawEntity> GetMany();
    LawEntity? GetByID(int id);
    LawEntity Create(CreateLawRequest req);
    LawEntity Update(UpdateLawRequest req);
    bool Delete(int id);
}

public class LawRepository(LegislationContext context) : ILawRepository
{
    public List<LawEntity> GetMany()
    {
        var laws = context.Laws
            .AsNoTracking()
            .Include(l => l.Referendum);
        return [.. laws];
    }

    public LawEntity? GetByID(int id)
    {
        return context.Laws
            .AsNoTracking()
            .Include(l => l.Referendum)
            .SingleOrDefault();
    }

    ///TODO: add better collision/duplicate handling?
    public LawEntity Create(CreateLawRequest req)
    {
        var referendum = context.Referendums
                            .AsNoTracking()
                            .Include(r => r.Laws)
                            .Single(r => r.ID == req.ReferendumID) ?? throw new ArgumentException("Referendum not found");
        
        var now = DateTime.UtcNow;
        var newEntity = new LawEntity()
        {
            ReferendumID = referendum.ID,
            Name = req.Name,
            YesVotes = req.YesVotes,
            NoVotes = req.NoVotes,
            CreatedAt = now,
            LastUpdated = now
        };
        var createdEntity = context.Laws.Add(newEntity);
        context.SaveChanges();
        return createdEntity.Entity;

    }

    public LawEntity Update(UpdateLawRequest req)
    {
        var law = context.Laws
            .AsNoTracking()
            .SingleOrDefault(l => l.ID == req.LawID) ?? throw new ArgumentException("Invalid Law ID");

        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            law.Name = req.Name;
        }

        //TODO: should this be plus equals instead of a hard replacement?
        law.YesVotes = req.YesVotes ?? law.YesVotes;
        law.NoVotes = req.NoVotes ?? law.NoVotes;

        var updatedEntity = context.Laws.Update(law);
        context.SaveChanges();
        return updatedEntity.Entity;
    }

    public bool Delete(int id)
    {
        var law = context.Laws
            .AsNoTracking()
            .SingleOrDefault(l => l.ID == id);

        if (law is null)
        {
            return false;
        }

        context.Laws.Remove(law);
        context.SaveChanges();
        return true;
    }
}
