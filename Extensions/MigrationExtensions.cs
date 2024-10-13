using DotnetIdentityDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace DotnetIdentityDemo.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        using IdentityContext identityContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
        using DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

        identityContext.Database.Migrate();
        databaseContext.Database.Migrate();
    }
}