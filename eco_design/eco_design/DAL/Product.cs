using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eco_design.DAL
{
    public class Product
    {
        public int ProductId { get; set; }
        public int VolumeOfProduction { get; set; } 
        public int Demand { get; set; } //спрос N
        public int CostOfProduct { get; set; } //стоимость C
        public virtual Company Company { get; set; }

    }
}
