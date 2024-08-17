using Microsoft.EntityFrameworkCore;
using Results.Data.ResultEntity;

namespace Results.Data;

public class ResultContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Result> Results { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new ResultConfiguration().Configure(modelBuilder.Entity<Result>());
    }
}
