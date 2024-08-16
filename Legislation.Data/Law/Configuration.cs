using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Legislation.Data.LawEntity;

public class LawConfiguration : IEntityTypeConfiguration<Law>
{
    public void Configure(EntityTypeBuilder<Law> builder)
    {
        builder
            .HasMany(l => l.Referendums)
            .WithOne(r => r.Law)
            .HasForeignKey(l => l.LawID)
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
