using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using PublicWorkflow.Application.Interfaces.Contexts;
using PublicWorkflow.Application.Interfaces.Shared;
using PublicWorkflow.Domain.Entities.Catalog;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PublicWorkflow.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<History> History { get; set; }
        public DbSet<Process> Process { get; set; }
        public DbSet<Approval> Approval { get; set; }
        public DbSet<ApprovalConfig> ApprovalConfig { get; set; }
        public DbSet<Organization> Organization { get; set; }
        public DbSet<OrganizationUser> OrganizationUser { get; set; }
        public DbSet<ProcessConfig> ProcessConfig { get; set; }
        public DbSet<ProcessRequirement> Requirement { get; set; }
        public DbSet<PublishOption> PublishOption { get; set; }
        public DbSet<ApprovalRule> ApprovalRule { get; set; }
        public DbSet<ProcessRule> ProcessRule { get; set; }

        #region Views
        public DbSet<ProcessView> ProcessView { get; set; }

        #endregion

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserName;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserName;
                        break;
                }
            }
            if (_authenticatedUser.UId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserName);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("application");

            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            //builder.Entity<History>(entity =>
            //{
            //    entity.ToTable(name: nameof(History).ToLower());
            //});
            //builder.Entity<Process>(entity =>
            //{
            //    entity.ToTable(name: nameof(Process).ToLower());
            //});
            
            //builder.Entity<Approval>(entity =>
            //{
            //    entity.ToTable(name: nameof(Approval).ToLower());
            //});

            //builder.Entity<ApprovalConfig>(entity =>
            //{
            //    entity.ToTable(name: nameof(ApprovalConfig).ToLower());
            //});

            //builder.Entity<Organization>(entity =>
            //{
            //    entity.ToTable(name: nameof(Organization).ToLower());
            //});

            //builder.Entity<OrganizationUser>(entity =>
            //{
            //    entity.ToTable(name: nameof(OrganizationUser).ToLower());
            //});

            //builder.Entity<ProcessConfig>(entity =>
            //{
            //    entity.ToTable(name: nameof(ProcessConfig).ToLower());
            //});

            //builder.Entity<ProcessRequirement>(entity =>
            //{
            //    entity.ToTable(name: nameof(ProcessRequirement).ToLower());
            //});

            //builder.Entity<PublishOption>(entity =>
            //{
            //    entity.ToTable(name: nameof(PublishOption).ToLower());
            //});
            //builder.Entity<ApprovalRule>(entity =>
            //{
            //    entity.ToTable(name: nameof(ApprovalRule).ToLower());
            //});
            //builder.Entity<ProcessRule>(entity =>
            //{
            //    entity.ToTable(name: nameof(ProcessRule).ToLower());
            //});

            builder.ApplyConfiguration(new ProcessConfiguration());
            builder.ApplyConfiguration(new ApprovalconfigConfiguration());
            builder.ApplyConfiguration(new ApprovalConfiguration());
            builder.ApplyConfiguration(new ProcessViewConfiguration());
            builder.Entity<ProcessView>().ToView(nameof(ProcessView).ToLower());
        }
    }
}