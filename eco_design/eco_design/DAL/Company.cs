using System.Collections.Generic;

namespace eco_design.DAL
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
