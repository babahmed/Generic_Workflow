using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PublicWorkflow.Infrastructure.DbContexts;
using PublicWorkflow.Infrastructure.Identity.Models;
using System;

namespace PublicWorkflow.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PublicWorkflow.Api");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
        }
        public static IApplicationBuilder UseDatabaseMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var services = serviceScope.ServiceProvider;
               // services.GetService<IdentityContext>().Database.Migrate();
                services.GetService<ApplicationDbContext>().Database.Migrate();
                
            }
            return app;
        }
    }
}