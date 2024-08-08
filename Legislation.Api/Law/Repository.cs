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
    LawEntity AddVotes(int lawID, int yesVotes, int noVotes);
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

    /// <summary>
    /// Finds and updates the entity using the law ID on req, will override the votes on the entity
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public LawEntity Update(UpdateLawRequest req)
    {
        var law = context.Laws
                    .AsNoTracking()
                    .SingleOrDefault(l => l.ID == req.LawID) ?? throw new ArgumentException("Invalid Law ID");

        if (!string.IsNullOrWhiteSpace(req.Name))
        {
            law.Name = req.Name;
        }

        law.YesVotes = req.YesVotes ?? law.YesVotes;
        law.NoVotes = req.NoVotes ?? law.NoVotes;

        var updatedEntity = context.Laws.Update(law);
        context.SaveChanges();
        return updatedEntity.Entity;
    }

    /// <summary>
    /// Similar to Update except adds the votes respectively instead of completely override them on the entity
    /// </summary>
    /// <exception cref="ArgumentException"></exception>
    public LawEntity AddVotes(int lawID, int yesVotes, int noVotes)
    {
        var law = context.Laws
                    .AsNoTracking()
                    .SingleOrDefault(l => l.ID == lawID) ?? throw new ArgumentException("Invalid Law ID");

        law.YesVotes += yesVotes;
        law.NoVotes += noVotes;

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
