using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSL.Extensor_Methods
{
    internal static class DoubleExtensions
    {
        public static bool IsInteger(this double value)
        {
            return Math.Abs(value % 1) < Double.Epsilon;
        }
    }
}
