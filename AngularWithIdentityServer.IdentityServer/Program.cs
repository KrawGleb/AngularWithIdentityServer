using System.Reflection;
using AngularWithIdentityServer.IdentityServer.Configurations;
using AngularWithIdentityServer.IdentityServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

services
    .AddIdentityServer()
    .AddTestUsers(InMemoryConfig.GetTestUsers())
    .AddDeveloperSigningCredential()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
            sql => sql.MigrationsAssembly(migrationAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"),
            sql => sql.MigrationsAssembly(migrationAssembly));
    });

var app = builder.Build();
    
app.MigrateDatabase();

app.UseIdentityServer();

app.Run();
