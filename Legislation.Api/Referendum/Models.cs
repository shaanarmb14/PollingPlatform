using LawEntity = Legislation.Data.Entities.Law;

namespace Legislation.Api.Referendum;

///TODO: refactor?
public record CreateReferendumRequest(string Name, List<LawEntity>? Laws);
public record UpdateReferendumRequest(int ReferendumID, string? Name, bool? Ended);
