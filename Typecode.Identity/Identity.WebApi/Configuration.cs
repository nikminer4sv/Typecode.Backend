using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.WebApi;

public static class Configuration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("TypecodeApi",new []
            {
                JwtClaimTypes.Name,
                JwtClaimTypes.Id,
                JwtClaimTypes.Email,
                JwtClaimTypes.Role
            })
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>
        {
            new ApiResource("TypecodeApi")
            {
                Name = "TypecodeApi",
                Enabled = true,
                ApiSecrets = new List<Secret>
                {
                    new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256())
                },
                Scopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId, 
                    IdentityServerConstants.StandardScopes.Profile,
                    "TypecodeApi"
                },
            }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new()
            {
                ClientId = "typecode-api",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = new List<Secret>
                {
                    new Secret("a75a559d-1dab-4c65-9bc0-f8e590cb388d".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "TypecodeApi"
                },
                AllowOfflineAccess = true
            }
        };
}