using Legislation.Data.Configuration;
using Legislation.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Legislation.Data;

public class LegislationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Referendum> Referendums { get; set; }
    public DbSet<Law> Laws { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new LawConfiguration().Configure(modelBuilder.Entity<Law>());
        new ReferendumConfiguration().Configure(modelBuilder.Entity<Referendum>());
    }
}
