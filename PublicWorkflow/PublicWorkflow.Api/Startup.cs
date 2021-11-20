using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PublicWorkflow.Api.Extensions;
using PublicWorkflow.Api.Filter;
using PublicWorkflow.Api.Middlewares;
using PublicWorkflow.Application.Extensions;
using PublicWorkflow.Infrastructure.Extensions;

namespace PublicWorkflow.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddContextInfrastructure(_configuration);
            services.AddPersistenceContexts(_configuration);
            services.AddRepositories();
            services.AddSharedInfrastructure(_configuration);
            services.AddEssentials();
            services.AddControllers();
            services.AddMvc(o =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                o.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddHangfireServer(
                c => new BackgroundJobServerOptions
                {
                    WorkerCount = 2,
                    Queues = new[] { "post-action" },
                });
            services.AddHangfireServer(
                c => new BackgroundJobServerOptions
                {
                    WorkerCount = 2,
                    Queues = new[] { "post-action" },
                });
            services.AddHangfireServer(
                c => new BackgroundJobServerOptions
                {
                    WorkerCount = 2,
                    Queues = new[] { "post-action" },
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                //Temporarily expose hangfire
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() },
                IgnoreAntiforgeryToken = true
            });
            app.UseHangfireDashboard();
            app.UseDatabaseMigrations();
            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseRouting();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}