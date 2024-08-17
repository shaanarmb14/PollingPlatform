using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Voting.Data.VotesEntity;

namespace Voting.Data.VoteEntity;

public class VoteConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        // Note postgress specific
        builder
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");
        builder
            .Property(r => r.LastUpdated)
            .HasDefaultValueSql("now() AT TIME ZONE 'UTC'");

        builder
            .Property(s => s.Choice)
            .HasConversion(
                v => v.ToString(),
                v => VoteChoiceExtensions.ParseFrom(v)
            );
    }

}
