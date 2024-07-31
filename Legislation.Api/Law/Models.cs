namespace Legislation.Api.Law;

///TODO: refactor?
public record CreateLawRequest(int ReferendumID, string Name, int Votes);
public record UpdateLawRequest(int LawID, string? Name, int? Votes);
