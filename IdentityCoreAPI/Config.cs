using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using System.Security.Claims;

namespace IdentityCoreAPI
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("document_api", "DocumentArchiverAPI")
        };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("document_api", "DocumentArchiverAPI")
            {
                Scopes = { "document_api" }
            }
        };

        public static IEnumerable<Client> Clients => new[]
        {
            new Client
            {
                ClientId = "document_archiver_client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "document_api" }
            }
        };

        public static List<TestUser> Users => new()
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "bashar",
                Password = "P@ssw0rdd",
                Claims = new List<Claim>
                {
                    new Claim("name", "Test User"),
                    new Claim("email", "test@example.com")
                }
            }
        };
    }
}
