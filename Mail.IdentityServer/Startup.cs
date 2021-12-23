using IdentityServer4;
using IdentityServer4.Services;
using Mail.IdentityServer.Data;
using Mail.IdentityServer.Infrastructure;
using Mail.IdentityServer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Mail.IdentityServer
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    //options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "374006741810-qj27n7s4342mgtbrq1bqgl1k0c1kbaqd.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            services.AddDbContext<DataContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)));
            })
                .AddIdentity<User, Role>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<DataContext>();

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
                config.Cookie.Name = "IdentityServer.Cookies";
            });

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString(nameof(DataContext)),
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();

            services.AddSingleton<ICorsPolicyService>((container) =>
            {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();     //CORE от IS4
                return new DefaultCorsPolicyService(logger)
                {
                    AllowedOrigins = { "http://localhost:4200" }
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();        //добавить IdentityServer в конвейер
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
