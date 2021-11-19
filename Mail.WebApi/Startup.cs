using Mail.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mail.Repository;
using Mail.Contracts.Repo;
using Mail.WebApi.Mappings;
using Mail.Contracts.Services;
using Mail.Services;
using Microsoft.Extensions.Logging;


namespace Mail.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
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

            services.AddAutoMapper(typeof(UserMapper));

            services.AddScoped<ILetterService, LetterService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();

            services.AddCors();
            services.AddControllers();
            services.AddMemoryCache();

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

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints => endpoints.MapControllers()); 
        }
    }
}
