using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Project3IdentityServer.var2
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
           new IdentityResource[]
           {
                new IdentityResources.OpenId()
           };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("openid"),
                new ApiScope("office.public", "Scope for public use (user registration)"),
                new ApiScope("office.admin", "Scope for admin"),
                new ApiScope("office.users", "Scope for users"),
                new ApiScope("office.read", "Scope for reading common data")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            { 
                // TODO ??? have a separate app for the admin, thus a separate client

                //new Client
                //{
                //    ClientId = "officeClient.admin",
                //    RequireClientSecret = false,
                //    RequirePkce = false, // TODO ...
                //    AllowedGrantTypes = GrantTypes.Code,
                //    RedirectUris = new[] { "http://localhost:4200" },
                //    PostLogoutRedirectUris = new[] { "http://localhost:4200" },
                //    AllowedScopes = new []
                //    {
                //        "openid",
                //        "office.admin",
                //        "office.read"
                //    },
                //    AllowedCorsOrigins = new[] { "http://localhost:4200", "https://localhost:4200",
                //                                 "http://20.114.173.131:80", "https://20.114.173.131:443" }
                //},

                new Client
                {
                    ClientId = "officeClient.users",
                    ClientName = "Office Resource Management",
                    RequireClientSecret = false,
                    RequirePkce = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new[] { "https://20.114.173.131:443" },
                    PostLogoutRedirectUris = new[] { "https://20.114.173.131:443" },
                    AllowedScopes = new []
                    {
                        "openid",
                        "office.users",
                        "office.read",
                        "office.admin"
                    },
                    AllowedCorsOrigins = new[] { "http://localhost:4200", "https://localhost:4200", 
                                                 "http://20.114.173.131:80", "https://20.114.173.131:443" }
                }
            };

        public static IEnumerable<TestUser> Users => 
            new TestUser[]
            {
                new TestUser() 
                { 
                    SubjectId = "827c77e9-23e2-427e-ee75-08da6fb06dbf",
                    Username = "Alex Toma", 
                    Password = "qwerty",
                    Claims = new List<Claim>
                    {
                        new Claim("Role", "0")
                    }
                },
                new TestUser()
                {
                    SubjectId = "3713dd38-977a-4d17-343b-08da73c2fa04",
                    Username = "Dorel Marcel",
                    Password = "qwertyuiop",
                    Claims = new List<Claim>
                    {
                        new Claim("Role", "0")
                    }
                },
                new TestUser()
                {
                    SubjectId = "e85f09b6-c3fb-4ec9-7d2e-08da748a24c8",
                    Username = "Gigica",
                    Password = "ceamaifainaparola",
                    Claims = new List<Claim>
                    {
                        new Claim("Role", "1")
                    }
                }
            };

        public static void MigrateInMemoryDataToSqlServer(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope())
            {
                if (scope == null)
                {
                    return;
                }

                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }

                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var apiscope in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(apiscope.ToEntity());
                    }

                    context.SaveChanges();
                }

            }
        }
    }
}
