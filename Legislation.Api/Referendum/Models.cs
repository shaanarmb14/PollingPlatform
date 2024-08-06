using LawEntity = Legislation.Data.LawEntity.Law;

namespace Legislation.Api.Referendum;

///TODO: refactor?
public record CreateReferendumRequest(string Name, List<LawEntity>? Laws);
public record UpdateReferendumRequest(int ReferendumID, string? Name, bool? Ended);
