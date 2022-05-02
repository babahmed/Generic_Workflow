using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using PublicWorkflow.Infrastructure.DbContexts;


namespace PublicWorkflow.Infrastructure.DbContexts
{
    public class DataInitializer
    {
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            //var databaseCreated = await db.Database.EnsureCreatedAsync();
            //if (databaseCreated)
            //{
            try
            {
                await GenerateProcessViews(db);
            }
            catch (Exception ex)
            {
                throw;
            }

            // }
        }

        private static async Task GenerateProcessViews(ApplicationDbContext context)
        {
            await context.Database.ExecuteSqlRawAsync(
                //Drop View and recreate

                @$"
                Drop View IF EXISTS application.processview;
                CREATE VIEW application.processview
                AS
                SELECT ap.id,
                ap.created_by,
                ap.created_on,
                ap.last_modified_by,
                ap.last_modified_on,
                ap.is_deleted,
                pr.id AS process_id,
                pr.attachements,
                pr.job_reference_id,
                pr.data,
                pr.status AS process_status,
                ap.status,
                pr.created_on AS logged,
                ap.already_approved AS raw_already_approved,
                ap.already_approved,
                ap.comments,
                ac.name AS level_name,
                ac.description AS level_description,
                ac.level,
                ac.required_approvers,
                ac.approvers AS raw_approvers,
                ac.approvers,
                pc.name AS process_name,
                pc.description AS process_description
                FROM application.approval ap
                JOIN application.process pr ON ap.process_id = pr.id
                JOIN application.approval_config ac ON ac.id = ap.approvalconfig_id
                JOIN application.process_config pc ON pc.id = ac.process_config_id;");
        }
    }
}