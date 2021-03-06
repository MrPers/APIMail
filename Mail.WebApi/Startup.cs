using Mail.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Mail.Repository;
using Mail.Contracts.Repo;
using Mail.WebApi.Mappings;
using Mail.Contracts.Services;
using Microsoft.Extensions.Logging;
using Mail.Business.Services;
using Mail.Business.Logics;
using Mail.Contracts.Logics;
using Mail.DTO.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using IdentityModel;

namespace Mail.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //зарегистрируем наш класс
            services.Configure<MySettingsModel>(Configuration.GetSection("MailConnection"));

            //логирование БД
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                      .AddConsole()
                      .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information);

                loggingBuilder
                      .AddDebug();
            });

            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IGroupRepository), typeof(GroupRepository));
            services.AddScoped(typeof(ILetterRepository), typeof(LetterRepository));
            services.AddScoped(typeof(ILetterStatusRepository), typeof(LetterStatusRepository));

            services.AddAutoMapper(typeof(UserMapper));

            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddScoped(typeof(ILetterLogics), typeof(LetterLogics));
            services.AddScoped(typeof(ICacheLogics), typeof(CacheLogics));

            services.AddCors();
            services.AddControllers();
            services.AddMemoryCache();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.FromSeconds(5)
                    };
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

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
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
