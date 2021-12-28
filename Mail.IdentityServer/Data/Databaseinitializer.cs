using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Mail.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mail.IdentityServer.Data
{
    public class DatabaseInitializer
    {
        public static void Init(IServiceProvider scopeServiceProvider)
        {

            scopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();

            if (!context.Clients.Any())
            {
                foreach (var client in IdentityServerConfiguration.GetClients())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in IdentityServerConfiguration.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            var contextNew = scopeServiceProvider.GetRequiredService<DataContext>();

            if (!contextNew.Users.Any() && !contextNew.Roles.Any())
            {
                context.Database.Migrate();

                var userManager = scopeServiceProvider.GetService<UserManager<User>>();
                var roleManager = scopeServiceProvider.GetService<RoleManager<Role>>();
                //var claimManager = scopeServiceProvider.GetService<ClimeManager<Claim>>();

                User[] users = new User[] {
                    new User("AdministratorALL"){Email = "iamanton@ukr.net"},
                    new User("AdministratorGroup"){Email = "iamanton@ukr.net"},
                    new User("AdministratorUser"){Email = "iamanton@ukr.net"},
                    new User("User"),
                };

                Role[] role = new Role[] {
                    new Role{Name = "Administrator"},
                    new Role{Name = "User"},
                };

                Claim[] claims = new Claim[] {
                    new Claim(JwtClaimTypes.Scope, "Group"),
                    new Claim(JwtClaimTypes.Scope, "User"),
                    new Claim(JwtClaimTypes.Scope, "Letter"),
                    //new Claim(){Type = JwtClaimTypes.Scope, Value = "Group" },
                    //new Claim(){Type = JwtClaimTypes.Scope, Value = "User" },
                    //new Claim(){Type = JwtClaimTypes.Scope, Value = "Letter" },
                };

                //contextNew.Claims.AddRange(claims);
                //context.SaveChanges();

                roleManager.CreateAsync(role[0]).GetAwaiter().GetResult();
                roleManager.CreateAsync(role[1]).GetAwaiter().GetResult();
                userManager.CreateAsync(users[0], "12qw!Q").GetAwaiter().GetResult();
                userManager.CreateAsync(users[1], "12qw!Q").GetAwaiter().GetResult();
                userManager.CreateAsync(users[2], "12qw!Q").GetAwaiter().GetResult();
                userManager.CreateAsync(users[3], "12qw!Q").GetAwaiter().GetResult();

                //contextNew.UserClaims.Add(new UserClaim() { User = users[0], Claim = claims[0] });
                //contextNew.UserClaims.Add(new UserClaim() { User = users[0], Claim = claims[1] });
                //contextNew.UserClaims.Add(new UserClaim() { User = users[0], Claim = claims[2] });
                //contextNew.UserClaims.Add(new UserClaim() { User = users[1], Claim = claims[0] });
                //contextNew.UserClaims.Add(new UserClaim() { User = users[2], Claim = claims[1] });
                userManager.AddClaimAsync(users[1], claims[0]).GetAwaiter().GetResult();
                userManager.AddClaimAsync(users[2], claims[1]).GetAwaiter().GetResult();
                userManager.AddClaimAsync(users[0], claims[0]).GetAwaiter().GetResult();
                userManager.AddClaimAsync(users[0], claims[2]).GetAwaiter().GetResult();
                userManager.AddClaimAsync(users[0], claims[1]).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[0], "Administrator").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[1], "Administrator").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[2], "Administrator").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[3], "User").GetAwaiter().GetResult();

                //context.SaveChanges();
            }
        }
    }
}