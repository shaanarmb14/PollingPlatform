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
            .Include(l => l.Referendums);
        return [.. laws];
    }

    public LawEntity? GetByID(int id)
    {
        return context.Laws
            .AsNoTracking()
            .Include(l => l.Referendums)
            .SingleOrDefault();
    }

    ///TODO: add better collision/duplicate handling?
    public LawEntity Create(CreateLawRequest req)
    {
        throw new NotImplementedException();

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
