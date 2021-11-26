using Mail.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using NLog.Web;

namespace Mail.WebApi
{
    public class Program
    {
        public static NLog.Logger Logger =
            NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<DataContext>();
                    SampleData.Initialize(context);
                }
                catch (Exception ex)
                {
                    Logger.Debug(ex, "An error occurred seeding the DB.");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog();
    }
}
