using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class Evaluation
    {

        private static double _median;
        private static double _sigma;
        public static double GetMedian(double[] array)
        {
            return _median = array.Length == 0 ? 0 : array.Sum(x => x) / array.Count();
        }

        public static double GetSigma(double[] array)
        {
            _median = GetMedian(array);
            return _sigma = array.Length == 0 ? 0 : array.Sum(x => Math.Pow(x - _median, 2));
        }
    }
}
