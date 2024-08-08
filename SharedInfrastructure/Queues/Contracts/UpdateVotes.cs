namespace Infrastructure.Queues.Contracts;

public record UpdateVotes(
    int LawID,
    int YesVotes,
    int NoVotes
);
