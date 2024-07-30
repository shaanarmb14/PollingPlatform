using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legislation.Data.Entities;

public class Referendum
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;
    [DefaultValue(false)]
    public bool Ended { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }

    public required List<Law> Laws { get; set; }
}
