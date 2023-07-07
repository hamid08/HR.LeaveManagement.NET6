using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HR.LeaveManagement.Identity.Configurations;
using System.Collections.Generic;
using System.Text;
using HR.LeaveManagement.Application.Models.Identity;

namespace HR.LeaveManagement.Identity
{
    public class LeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public LeaveManagementIdentityDbContext(DbContextOptions<LeaveManagementIdentityDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRefreshTokenConfiguration());
        }
    }
}
