using Legislation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legislation.Data.Configuration;

public class LawConfiguration : IEntityTypeConfiguration<Law>
{
    public void Configure(EntityTypeBuilder<Law> builder)
    {
        builder
            .HasIndex(l => l.ReferendumID)
            .HasDatabaseName("IX_LawEntity_ReferendumId");

        builder
            .HasOne(l => l.Referendum)
            .WithMany(r => r.Laws)
            .HasForeignKey(l => l.ReferendumID)
            .IsRequired();

        // Note postgress specific
        builder
            .Property(l => l.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(l => l.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

        builder
            .Property(l => l.YesVotes)
            .HasDefaultValue(0);
        builder
            .Property(l => l.NoVotes)
            .HasDefaultValue(0);
    }
}
