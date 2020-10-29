using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class Evaluation
    {
        public static double GetMedian(double[] array)
        {
            return array.Count() == 0 ? 0 : array.Sum(x => x) / array.Count();
        }
    }
}
