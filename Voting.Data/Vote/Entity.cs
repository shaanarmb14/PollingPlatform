using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Voting.Data.VoteEntity;

public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public VoteChoice Choice { get; set; } = VoteChoice.Unknown;
    public int ReferendumID { get; set; }
}

public enum VoteChoice
{
    Yes,
    No,
    Abstain,
    Unknown
}
