using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicWorkflow.Domain.Entities.Catalog;
using System;

namespace PublicWorkflow.Infrastructure.DbContexts
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> builder)
        {

            builder
            .Property(e => e.Attachements)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));
        }
    }
    public class ApprovalconfigConfiguration : IEntityTypeConfiguration<ApprovalConfig>
    {
        public void Configure(EntityTypeBuilder<ApprovalConfig> builder)
        {

            builder
            .Property(e => e.Approvers)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public class ApprovalConfiguration : IEntityTypeConfiguration<Approval>
    {
        public void Configure(EntityTypeBuilder<Approval> builder)
        {
            builder
            .Property(e => e.AlreadyApproved)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));

            builder
            .Property(e => e.Comments)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));
        }
    }

    public class ProcessViewConfiguration : IEntityTypeConfiguration<ProcessView>
    {
        public void Configure(EntityTypeBuilder<ProcessView> builder)
        {
            builder
            .Property(e => e.AlreadyApproved)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));

            builder
            .Property(e => e.Comments)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));

            builder
            .Property(e => e.Attachements)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));

            builder
            .Property(e => e.Approvers)
            .HasConversion(
                v => string.Join('|', v),
                v => v.Split('|', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
