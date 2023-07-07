using HR.LeaveManagement.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
    public static class IdentityDbInitializer
    {
        public static async Task IdentityInitialize(this IServiceProvider serviceProvider)
        {
            var leaveManagementIdentityDbContext = serviceProvider.GetRequiredService<LeaveManagementIdentityDbContext>();

            // Here is the migration executed
            leaveManagementIdentityDbContext.Database.Migrate();


        }
    }
}
