using Voting.Data;
using Voting.Data.VoteEntity;

namespace Voting.Api;

public interface IVoteRepository
{
    Vote Create(VoteRequest dto);
}

public class VoteRepository(VoteContext context) : IVoteRepository
{
    public Vote Create(VoteRequest dto)
    {
        var now = DateTime.UtcNow;
        var newVote = new Vote
        {
            Choice = dto.Choice,
            // no way to validate that this is a valid referendum
            ReferendumID = dto.ReferendumID,
            CreatedAt = now,
            LastUpdated = now
        };

        var createdEntity = context.Votes.Add(newVote);
        context.SaveChanges();

        return createdEntity.Entity;
    }
}
