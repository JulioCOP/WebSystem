using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SystemSales.Models
{
    public class SystemSalesContext : DbContext
    {
        public SystemSalesContext (DbContextOptions<SystemSalesContext> options)
            : base(options)
        {
        }

        public DbSet<SystemSales.Models.Department> Department { get; set; }
    }
}
