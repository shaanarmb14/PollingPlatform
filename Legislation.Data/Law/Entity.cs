using Legislation.Data.ReferendumEntity;
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
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
    public required List<Referendum> Referendums { get; set; } = [];
}
