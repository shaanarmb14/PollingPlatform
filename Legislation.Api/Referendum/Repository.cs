using Microsoft.EntityFrameworkCore;
using Legislation.Data;
using ReferendumEntity = Legislation.Data.Entities.Referendum;

namespace Legislation.Api.Referendum;

public interface IReferendumRepository
{
    List<ReferendumEntity> GetMany();
    ReferendumEntity GetByID(int id);
    Task<ReferendumEntity> Create();
    Task<ReferendumEntity> Update();
    bool Delete(int id);
}
public class ReferendumRepository(LegislationContext context) : IReferendumRepository
{
    public List<ReferendumEntity> GetMany() 
    {
        return [..context.Referendums.AsNoTracking()];
    }

    public ReferendumEntity GetByID(int id) 
    {
        return context.Referendums
            .AsNoTracking()
            .Single();
    }

    ///TODO: DTOs?
    public Task<ReferendumEntity> Create() { throw new NotImplementedException(); }

    ///TODO: DTOs?
    public Task<ReferendumEntity> Update() { throw new NotImplementedException(); }

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
