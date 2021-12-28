using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Mail.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {//Получает или задает параметры, используемые для проверки токенов удостоверения.
                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
                    config.RequireHttpsMetadata = false;//Gets or sets if HTTPS is required for the metadata address or authority
                    config.Authority = "https://localhost:10001";
                    config.Audience = "Order";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("GroupAdministrator", authBuilder =>
                {
                    authBuilder.RequireRole("Administrator");
                    authBuilder.RequireClaim("Group", "True");
                });
                options.AddPolicy("UserAdministrator", authBuilder =>
                {
                    authBuilder.RequireRole("Administrator");
                    authBuilder.RequireClaim("User", "True");
                });
                options.AddPolicy("ALLAdministrator", authBuilder =>
                {
                    authBuilder.RequireRole("Administrator");
                    authBuilder.RequireClaim("Group", "True");
                    authBuilder.RequireClaim("User", "True");
                    authBuilder.RequireClaim("Letter", "True");
                });
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}