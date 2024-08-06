using Legislation.Data.ReferendumEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legislation.Data.LawEntity;

public class Law
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;
    [DefaultValue(0)]
    public int YesVotes { get; set; } = 0;
    public int NoVotes { get; set; } = 0;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }

    public int TotalVotes => YesVotes + NoVotes;

    public int ReferendumID { get; set; }
    public Referendum Referendum { get; set; } = null!;
}
