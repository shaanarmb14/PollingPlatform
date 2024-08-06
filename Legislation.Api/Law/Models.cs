namespace Legislation.Api.Law;

///TODO: refactor?
public record CreateLawRequest(int ReferendumID, string Name, int YesVotes, int NoVotes);
public record UpdateLawRequest(int LawID, string? Name, int? YesVotes, int? NoVotes);
