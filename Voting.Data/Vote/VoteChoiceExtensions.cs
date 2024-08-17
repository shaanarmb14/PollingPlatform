using Voting.Data.VoteEntity;

namespace Voting.Data.VotesEntity
{
    public static class VoteChoiceExtensions
    {
        public static VoteChoice ParseFrom(string value) => Enum.TryParse<VoteChoice>(value, out var result) ? result : VoteChoice.Unknown;
    }
}
