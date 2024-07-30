using Microsoft.EntityFrameworkCore;
using Legislation.Data;
using LawEntity = Legislation.Data.Entities.Law;

namespace Legislation.Api.Law;

public interface ILawRepository 
{ 
    List<LawEntity> GetMany();
    LawEntity? GetByID(int id);
    Task<LawEntity> Create();
    Task<LawEntity> Update();
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
    public Task<LawEntity> Create()
    {
        throw new NotImplementedException();
    }

    ///TODO: do I want to use a DTO?
    public Task<LawEntity> Update()
    {
        throw new NotImplementedException();
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
