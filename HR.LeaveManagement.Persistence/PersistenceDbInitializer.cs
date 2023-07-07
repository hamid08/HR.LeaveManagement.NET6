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
    public static class PersistenceDbInitializer
    {
        public static async Task PersistenceInitialize(this IServiceProvider serviceProvider)
        {
            var leaveManagementDbContext = serviceProvider.GetRequiredService<LeaveManagementDbContext>();

            // Here is the migration executed
            leaveManagementDbContext.Database.Migrate();


        }
    }
}
