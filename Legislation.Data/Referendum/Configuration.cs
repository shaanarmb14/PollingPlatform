using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legislation.Data.ReferendumEntity;

public class ReferendumConfiguration : IEntityTypeConfiguration<Referendum>
{
    public void Configure(EntityTypeBuilder<Referendum> builder)
    {
        builder
            .HasIndex(l => l.LawID)
            .HasDatabaseName("IX_ReferendumEntity_LawID");

        // Note postgress specific
        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(r => r.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

        builder
            .Property(r => r.Open)
            .HasDefaultValue(true);
    }
}