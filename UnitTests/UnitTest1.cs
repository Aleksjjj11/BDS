using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Mime;

namespace UnitTests
{
    public class Tests
    {

        [Test]
        public void MedianOfArray_MustReturn_2()
        {
            double[] testArr = { 1, 2, 3 };
            double median = Evaluation.GetMedian(testArr);
            Assert.AreEqual(2, median);
        }

        [Test]
        public void MedianOfArray_MustReturn_5()
        {
            double[] testArr = { 5, 5, 5, 10, 0 };
            double median = Evaluation.GetMedian(testArr);
            Assert.AreEqual(5, median);
        }
    }

    public static class Evaluation
    {
        public static double GetMedian(double[] array)
        {
            return array.Sum(x => x) / array.Count();
        }
    }
}