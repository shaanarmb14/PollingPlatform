using Microsoft.EntityFrameworkCore;
using Voting.Data.VoteEntity;

namespace Voting.Data;

public class VoteContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Vote> Votes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new VoteConfiguration().Configure(modelBuilder.Entity<Vote>());
    }
}
