using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Results.Data.ResultEntity;

public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        // Note postgress specific
        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(r => r.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
    }
}
