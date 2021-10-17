using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PublicWorkflow.Infrastructure.DbContexts;
using System;
using System.Threading.Tasks;

namespace PublicWorkflow.Infrastructure.Extensions
{
    public class Helper
    {
        public async Task InitializeDatabaseAsync(IServiceProvider serviceProvider, IConfiguration config)
        {
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var databaseCreated = await db.Database.EnsureCreatedAsync();
            if (databaseCreated)
            {
                await CreateApprovalsView(db, config["dbUserName"]);
            }
        }

        private static async Task CreateApprovalsView(ApplicationDbContext context, string username)
        {
            await context.Database.ExecuteSqlRawAsync(
                $"CREATE OR REPLACE VIEW public.\"ProcessView\"ASselect \"ap\".\"Id\",\"ap\".\"CreatedBy\",\"ap\".\"CreatedOn\",\"ap\".\"LastModifiedBy\",\"ap\".\"LastModifiedOn\",\"ap\".\"IsDeleted\",\"pr\".\"Id\"as\"ProcessId\",\"pr\".\"Attachements\",\"pr\".\"JobReferenceId\",\"pr\".\"Data\",\"pr\".\"Status\"as\"ProcessStatus\",\"ap\".\"Status\",\"pr\".\"CreatedOn\"as\"Logged\",\"ap\".\"AlreadyApproved\",\"ap\".\"Comments\",\"ac\".\"Name\"as\"LevelName\",\"ac\".\"Description\"as\"LevelDescription\"\"ac\".\"Level\",\"ac\".\"RequiredApprovers\",\"ac\".\"Approvers\",\"pc\".\"Name\"as\"ProcessName\",\"pc\".\"Description\"as\"ProcessDescription\"from public.\"Approval\"\"ap\"join public.\"Process\"\"pr\"on\"ap\".\"ProcessId\"=\"pr\".\"Id\"join public.\"ApprovalConfig\"\"ac\"on\"ac\".\"Id\"=\"ap\".\"ApprovalconfigId\"join public.\"ProcessConfig\"\"pc\"on\"pc\".\"Id\"=\"ac\".\"ProcessConfigId\";ALTER TABLE public.\"ProcessView\"OWNER TO {username};");
        }
    }
}
