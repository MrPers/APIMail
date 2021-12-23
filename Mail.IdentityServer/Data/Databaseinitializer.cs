using System;
using System.Linq;
using System.Security.Claims;
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

                User[] users = new User[] {
                    new User("AdministratorLetter"){Email = "iamanton@ukr.net"},
                    new User("AdministratorALL"),
                    new User("User"),
                    //new User("User1"){Email = "iamanton@ukr.net"},
                    //new User("User2"){Email = "iamanton@ukr.net"},
                };

                Role[] role = new Role[] {
                    new Role{Name = "Administrator"},
                    new Role{Name = "User"},
                };

                //var result1 = userManager.CreateAsync(users[0], "12qw!Q").GetAwaiter().GetResult().Succeeded;
                userManager.CreateAsync(users[0], "12qw!Q").GetAwaiter().GetResult();
                userManager.CreateAsync(users[1], "12qw!Q").GetAwaiter().GetResult();
                userManager.CreateAsync(users[2], "12qw!Q").GetAwaiter().GetResult();
                //userManager.CreateAsync(users[3], "12qw!Q").GetAwaiter().GetResult();
                roleManager.CreateAsync(role[0]).GetAwaiter().GetResult();
                roleManager.CreateAsync(role[1]).GetAwaiter().GetResult();
                //userManager.AddClaimAsync(users[0], new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
                //userManager.AddClaimAsync(users[1], new Claim(ClaimTypes.Role, "User")).GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[0], "Administrator").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[1], "Administrator").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(users[2], "User").GetAwaiter().GetResult();
                //userManager.AddToRoleAsync(users[3], "User").GetAwaiter().GetResult();
                userManager.AddClaimAsync(users[0], new Claim(JwtClaimTypes.Scope, "Order")).GetAwaiter().GetResult();
            }
        }
    }
}