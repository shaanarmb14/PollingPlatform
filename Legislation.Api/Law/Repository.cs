using Microsoft.EntityFrameworkCore;
using Legislation.Data;
using LawEntity = Legislation.Data.Entities.Law;

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
        return [.. context.Laws.AsNoTracking()];
    }

    public LawEntity? GetByID(int id)
    {
        return context.Laws
            .AsNoTracking()
            .SingleOrDefault();
    }

    ///TODO: do I want to use a DTO?
    public LawEntity Create(CreateLawRequest req)
    {
        var referendum = context.Referendums
                            .AsNoTracking()
                            .Include(r => r.Laws)
                            .Single(r => r.ID == req.ReferendumID) ?? throw new ArgumentException("Referendum not found");
        
        var newEntity = new LawEntity()
        {
            ReferendumID = referendum.ID,
            Name = req.Name,
            Votes = req.Votes
        };
        var createdEntity = context.Laws.Add(newEntity);
        context.SaveChanges();
        return createdEntity.Entity;

    }

    ///TODO: do I want to use a DTO?
    public LawEntity Update(UpdateLawRequest req)
    {
        var law = context.Laws
            .AsNoTracking()
            .SingleOrDefault(l => l.ID == req.LawID) ?? throw new ArgumentException("Invalid Law ID");

        if (req.Name is not null && req.Name != string.Empty)
        {
            law.Name = req.Name;
        }
        law.Votes = req.Votes;

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
