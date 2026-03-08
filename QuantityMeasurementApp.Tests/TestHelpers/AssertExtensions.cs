using NUnit.Framework;

namespace QuantityMeasurementApp.Tests.TestHelpers
{
    public static class AssertExtensions
    {
        public static void AreClose(double expected, double actual)
        {
            Assert.AreEqual(expected, actual, 0.01);
        }
    }
}