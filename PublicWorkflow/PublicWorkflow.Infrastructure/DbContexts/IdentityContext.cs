using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PublicWorkflow.Infrastructure.Identity.Models;

namespace PublicWorkflow.Infrastructure.DbContexts
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("identity");
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "users");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "roles");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("user_roles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("user_claims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("user_logins");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("role_claims");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("user_tokens");
            });
        }
    }
}