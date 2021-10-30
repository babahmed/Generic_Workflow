using AspNetCoreHero.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PublicWorkflow.Infrastructure.DbContexts;
using PublicWorkflow.Infrastructure.Identity.Models;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PublicWorkflow.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // reloadOnChange Whether the configuration should be reloaded if the file changes.
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables() // Environment Variables override all other, ** THIS SHOULD ALWAYS BE LAST
            .Build();
        public static async Task Main(string[] args)
        {

            //Log.Logger = new LoggerConfiguration()
            //.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            //.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            //.Enrich.FromLogContext()
            //.Enrich.WithProperty("Service", "workflow")
            ////.WriteTo.Seq(logurl)
            //.CreateLogger();

            try
            {

                var host = CreateHostBuilder(args).Build();


                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    services.GetService<IdentityContext>().Database.Migrate();
                    try
                    {
                       // Serilog.Log.Information($"Attempting to seed data");

                        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                        await Infrastructure.Identity.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
                        await Infrastructure.Identity.Seeds.DefaultBasicUser.SeedAsync(userManager, roleManager);
                       // Serilog.Log.Information("Finished Seeding Default Data");
                       // Serilog.Log.Information($"Starting up {ApplicationName}");
                    }
                    catch (Exception ex)
                    {
                        //Serilog.Log.Warning(ex, "An error occurred seeding the DB");
                        throw;
                    }
                }
                host.Run();
            }
            catch (Exception ex)
            {
                //Serilog.Log.Fatal<Exception>($"Application workflow start-up failed", ex);
                throw;
            }
            //finally
            //{
            //    Log.CloseAndFlush();
            //}

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //.UseSerilog()
            .ConfigureAppConfiguration((c, x) =>
            {

                x.AddConfiguration(Configuration);

            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}