using Microsoft.EntityFrameworkCore;
using Voting.Data;

namespace Voting.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using VoteContext dbContext = scope.ServiceProvider.GetRequiredService<VoteContext>();

        dbContext.Database.Migrate();
    }
}
