using AngularWithIdentityServer.IdentityServer.Configurations;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;

namespace AngularWithIdentityServer.IdentityServer.Extensions;

public static class MigrationManagerExtension
{
    public static IHost MigrateDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

        using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        try
        {
            context.Database.Migrate();

            if (!context.Clients.Any())
            {
                foreach (var client in InMemoryConfig.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in InMemoryConfig.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var apiScope in InMemoryConfig.GetApiScopes())
                {
                    context.ApiScopes.Add(apiScope.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in InMemoryConfig.GetApiResources())
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }
                context.SaveChanges();
            }
        }
        catch 
        {
            // TODO: Log errors
        }

        return host;
    }
}
