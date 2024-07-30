using Legislation.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legislation.Data.Configuration;

public class ReferendumConfiguration : IEntityTypeConfiguration<Referendum>
{
    public void Configure(EntityTypeBuilder<Referendum> builder)
    {
        builder
            .HasMany(r => r.Laws)
            .WithOne(l => l.Referendum)
            .HasForeignKey(l => l.ReferendumID)
            .IsRequired();

        // Note postgress specific
        builder
            .Property(l => l.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(l => l.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
    }
}
