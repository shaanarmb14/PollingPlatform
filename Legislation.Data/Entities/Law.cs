﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legislation.Data.Entities;

public class Law
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; init; }
    [Column(TypeName = "varchar(100)")]
    public string Name { get; init; } = string.Empty;
    public int Votes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdated { get; init; }

    public int ReferendumID { get; init; }
    public required Referendum Referendum { get; init; }
}
