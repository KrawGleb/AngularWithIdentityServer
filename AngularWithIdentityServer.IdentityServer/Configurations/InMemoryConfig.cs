using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace AngularWithIdentityServer.IdentityServer.Configurations;

public static class InMemoryConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static List<TestUser> GetTestUsers() =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "C453D7B4-C493-47D9-A863-6677B3F8BB8B",
                Username = "Mick",
                Password = "MickPassword",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Mick"),
                    new Claim("family_name", "Mining"),
                },
            },
            new TestUser
            {
                SubjectId = "BF7A8027-1181-4BA9-B6DE-1988141125B5",
                Username = "Jane",
                Password = "JanePassword",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "Jane"),
                    new Claim("family_name", "Downing"),
                },
            },
        };

    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "company-employee",
                ClientSecrets = new [] { new Secret("secret".Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "companyApi" },
            },
            new Client
            {
                ClientName = "Angular Client",
                ClientId = "angular-client",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RedirectUris = new List<string> { "https://localhost:5010/sigin-oidc" },
                RequirePkce = false,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile },
                ClientSecrets = new [] { new Secret("angularSecret".Sha512()) },
            },
        };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope("companyApi", "CompanyEmployee API"),
        };

    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new ApiResource("companyApi", "CompanyEmployee API")
            {
                Scopes = { "companyApi" },
            },
        };
}
