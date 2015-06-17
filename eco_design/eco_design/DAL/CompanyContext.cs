using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eco_design.DAL
{
    public class CompanyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
