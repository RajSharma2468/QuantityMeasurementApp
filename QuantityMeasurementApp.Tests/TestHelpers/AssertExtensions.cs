using NUnit.Framework;

namespace QuantityMeasurementApp.Tests.TestHelpers
{
    public static class AssertExtensions
    {
        public static void AreAlmostEqual(double expected, double actual, double epsilon = 0.001)
        {
            Assert.That(System.Math.Abs(expected - actual) < epsilon);
        }
    }
}