using PublicWorkflow.Infrastructure.Identity.Models;
using AspNetCoreHero.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;
using PublicWorkflow.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace PublicWorkflow.Api
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: false)
                 .Build();

            string logurl = config.GetValue<string>("logurl");
            string ApplicationName = config.GetValue<string>("ApplicationName");

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Service", ApplicationName)
            .WriteTo.Seq(logurl)
            .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    services.GetService<IdentityContext>().Database.Migrate();
                    try
                    {
                        Serilog.Log.Information($"Attempting to seed data");

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                        await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
                        Serilog.Log.Information("Finished Seeding Default Data");
                        Serilog.Log.Information($"Starting up {ApplicationName}");
                    }
                    catch (Exception ex)
                    {
                        Serilog.Log.Warning(ex, "An error occurred seeding the DB");
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal<Exception>($"Application {ApplicationName} start-up failed", ex);
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}