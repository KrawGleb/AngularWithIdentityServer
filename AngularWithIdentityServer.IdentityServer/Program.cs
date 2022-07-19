using AngularWithIdentityServer.IdentityServer.Configurations;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


// TODO: Change memory storage with db
services.AddIdentityServer()
    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
    .AddTestUsers(InMemoryConfig.GetTestUsers())
    .AddInMemoryClients(InMemoryConfig.GetClients())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseIdentityServer();

app.Run();
