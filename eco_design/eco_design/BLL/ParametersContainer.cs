using eco_design.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eco_design.BLL
{
    public class ParametersContainer
    {
        public Product CompanyProduct { get; set; }

        public Product CompanyNewEcoProduct { get; set; }

        public Product ConcurentProduct { get; set; }

        public double[,] X { get; set; }

        public double[,] Xs { get; set; }
    }
}
