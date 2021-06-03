using Microsoft.AspNetCore.Builder;

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
    }
}