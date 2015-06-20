namespace eco_design.BLL
{
    public class Algorithm
    {
        public double[,] A { get; set; } // 2 х 3

        public double[,] N { get; set; } // 2 x 3

        public double[,] T { get; set; } // 2 x 4

        public double[,] K { get; set; } // 2 x 8

        public void Find(ParametersContainer container)
        {
            //var product1 = container.CompanyProduct;
            //var product2 = container.ConcurentProduct;
            //var newProduct = container.CompanyNewEcoProduct;

            //var volumeSum = product1.VolumeOfProduction + product2.VolumeOfProduction;

            //var beta11 = product1.VolumeOfProduction / volumeSum;
            //var beta21 = product2.VolumeOfProduction / volumeSum;

            //if (beta21 <= beta11)
            //{
            //    throw new ComparisonException(ComparisonType.FirstVolumeBiggestThanSecond);
            //}

            //var gamma11 = product1.Demand / (product1.Demand + product2.Demand);
            //var gamma21 = product1.Demand / (product1.Demand + product2.Demand);

            //if (gamma21 <= gamma11)
            //{
            //    throw new ComparisonException(ComparisonType.FirstDemandBiggestThanSecond);
            //}

            //var costCoefficient = product1.CostOfProduct / product2.CostOfProduct;

            //if (costCoefficient >= 1)
            //{
            //    throw new ComparisonException(ComparisonType.FirstCostBiggestThanSecond);
            //}

            //var b11 = product1.CostOfProduct * gamma11 * volumeSum;
            //var b21 = product2.CostOfProduct * gamma21 * volumeSum;

            //var bS11 = product1.CostOfProduct * product1.VolumeOfProduction;
            //var bS21 = product2.CostOfProduct * product2.VolumeOfProduction;


        }

        public double SumF(double[] x, double[] xs, Parameter param)
        {
            double res1 = I(x, xs, param);
            double res2 = 0;

            int p = (int) param;

            for (int i = 0; i < 3; i++)
            {
                res1 *= 1 - N[p, i];

                res2 += N[p, i] * J(x, xs, A[p, i], param);
            }

            double res3 = res1 + res2;

            return res3;
        }

        public double J(double[] x, double[] xs, double a, Parameter param)
        {
            var res = a*I(x, xs, param);

            return res;
        }

        public double I(double[] x, double[] xs, Parameter param)
        {
            double res = 0;

            switch (param)
            {
                case Parameter.X12:
                    res = (F12(x, xs) - Fm12(x, xs))/(Fp12(x, xs) - Fm12(x, xs));
                    break;
                    
                case Parameter.X21:
                    res = (F21(x, xs) - Fm21(x, xs))/(Fp21(x, xs) - Fm21(x, xs));
                    break;
            }

            return res;
        }

        public double F12(double[] x, double[] xs)
        {
            return T[0, 0]*K[0, 0]*x[0] + T[0, 1]*K[0, 2]*x[1] + T[0, 2]*K[1, 0]*xs[0] - T[0, 3]*K[1, 2]*xs[1];
        }

        public double F21(double[] x, double[] xs)
        {
            return T[1, 0] * K[1, 0] * x[0] + T[1, 1] * K[1, 2] * x[1] - T[1, 2] * K[0, 0] * xs[0] - T[1, 3] * K[0, 2] * xs[1];
        }

        public double Fp12(double[] x, double[] xs)
        {
            return K[0, 1]*x[0] + K[0, 3]*x[1] + K[0, 5]*xs[0] - K[0, 7]*xs[1];
        }

        public double Fm12(double[] x, double[] xs)
        {
            return K[0, 0] * x[0] + K[0, 2] * x[1] + K[0, 4] * xs[0] - K[0, 6] * xs[1];
        }

        public double Fp21(double[] x, double[] xs)
        {
            return K[1, 1] * x[0] + K[1, 3] * x[1] - K[1, 5] * xs[0] - K[1, 7] * xs[1];
        }

        public double Fm21(double[] x, double[] xs)
        {
            return K[1, 0] * x[0] + K[1, 2] * x[1] - K[1, 4] * xs[0] - K[1, 6] * xs[1];
        }
    }

    public enum Parameter
    {
        X12,
        X21
    }
}
