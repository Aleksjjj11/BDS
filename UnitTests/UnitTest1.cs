using BusinessLogic;
using NUnit.Framework;

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

        [Test]
        public void MedianOfEmptyArray_MustReturn_0()
        {
            double[] testArr = { 5, 5, 5, 10, 0 };
            double median = Evaluation.GetMedian(testArr);
            Assert.AreEqual(5, median);
        }

        [Test]
        public void SigmaOf_11_10_9_MustReturn_2()
        {
            double[] testArr = {11, 10, 9};
            Evaluation.GetMedian(testArr);
            double sigma = Evaluation.GetSigma(testArr);
            Assert.AreEqual(2, sigma);
        }
    }

}