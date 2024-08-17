using Voting.Data.VoteEntity;

namespace Voting.Api;

public record VoteRequest(VoteChoice Choice, int ReferendumID) 
{
    public bool Validate()
    {
        return ReferendumID > 0;
    }
}
