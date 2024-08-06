namespace SharedInfrastructure.Queues.Contracts;

public record UpdateVotes(
    int LawID,
    int YesVotes,
    int NoVotes
);
