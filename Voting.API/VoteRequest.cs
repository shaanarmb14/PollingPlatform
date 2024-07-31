using SharedInfrastructure.Queues.Contracts;

namespace Voting.Api;

public record VoteRequest(int LawID, int Votes) 
{
    public UpdateVotes ToMessage() => new(LawID, Votes);
}
