using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        DbSet<History> History { get; set; }
        DbSet<Process> Process { get; set; }
        DbSet<Approval> Approval { get; set; }
        DbSet<ApprovalConfig> ApprovalConfig { get; set; }
        DbSet<Organization> Organization { get; set; }
        DbSet<OrganizationUser> OrganizationUser { get; set; }
        DbSet<ProcessConfig> ProcessConfig { get; set; }
        DbSet<ProcessRequirement> Requirement { get; set; }
        DbSet<PublishOption> PublishOption { get; set; }
        DbSet<ProcessView> ProcessView { get; set; }
    }
}