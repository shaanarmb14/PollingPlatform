using System.Collections.Immutable;

namespace Auth;

public static class Roles
{
    public const string LegislatorRole = "Legislator";
    public const string CitizenRole = "Citizen";

    public static readonly ImmutableList<string> AllRoles = [LegislatorRole, CitizenRole];
}
