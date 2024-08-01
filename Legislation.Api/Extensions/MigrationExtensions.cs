using Legislation.Data;
using Microsoft.EntityFrameworkCore;

namespace Legislation.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using LegislationContext dbContext = scope.ServiceProvider.GetRequiredService<LegislationContext>();

        dbContext.Database.Migrate();
    }
}
