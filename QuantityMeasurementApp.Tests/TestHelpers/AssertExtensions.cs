using Xunit;
using System;

namespace QuantityMeasurementApp.Tests.TestHelpers;

public static class AssertExtensions
{
    public static void ApproximatelyEqual(double expected, double actual, double tolerance = 1e-10)
    {
        Assert.True(Math.Abs(expected - actual) < tolerance, 
            $"Expected: {expected}, Actual: {actual}");
    }
}