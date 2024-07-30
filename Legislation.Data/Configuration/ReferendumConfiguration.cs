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
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(r => r.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

        // Not sure if this is doubling up with [DefaultValue] attribute
        builder
            .Property(r => r.Ended)
            .HasDefaultValue(false);
    }
}
