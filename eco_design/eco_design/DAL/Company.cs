using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eco_design.DAL
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
