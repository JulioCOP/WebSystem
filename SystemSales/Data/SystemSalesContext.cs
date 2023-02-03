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

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecords { get; set; }
    }
}
