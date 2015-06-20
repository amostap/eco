﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eco_design.BLL
{
    public class ComparisonException : Exception
    {
        public ComparisonType ComparisonType { get; set; }

        public ComparisonException(ComparisonType comparisonType) : base("An comparison exception has occured. See the ComparisonType who occure this exception.")
        {
            ComparisonType = comparisonType;
        }
    }

    public enum ComparisonType
    {
        FirstVolumeBiggestThanSecond,
        FirstDemandBiggestThanSecond,
        FirstCostBiggestThanSecond
    }
}
