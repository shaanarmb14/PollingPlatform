﻿using Legislation.Data.LawEntity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legislation.Data.ReferendumEntity;

public class Referendum
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;
    [DefaultValue(false)]
    public bool Open { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public int LawID { get; set; }
    public Law Law { get; set; } = null!;
}
