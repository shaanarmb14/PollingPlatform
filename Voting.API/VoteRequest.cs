using Infrastructure.Queues.Contracts;

namespace Voting.Api;

public record VoteRequest(int LawID, int YesVotes, int NoVotes) 
{
    public bool Validate()
    {
        return LawID > 0;
    }

    public UpdateVotes ToMessage() => new(LawID, YesVotes, NoVotes);
}
