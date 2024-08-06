using SharedInfrastructure.Queues.Contracts;

namespace Voting.Api;

public record VoteRequest(int LawID, int YesVotes, int NoVotes) 
{
    public UpdateVotes ToMessage() => new(LawID, YesVotes, NoVotes);
}
