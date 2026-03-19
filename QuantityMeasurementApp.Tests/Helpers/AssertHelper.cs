using Xunit;

namespace QuantityMeasurementApp.Tests.Helpers;

public static class AssertHelper
{
    public static void ShouldBeSuccessful(QuantityResultDTO result)
    {
        Assert.NotNull(result);
        Assert.False(result.IsError);
        Assert.Null(result.ErrorMessage);
    }

    public static void ShouldHaveError(QuantityResultDTO result, string expectedErrorContains = null)
    {
        Assert.NotNull(result);
        Assert.True(result.IsError);
        Assert.NotNull(result.ErrorMessage);
        
        if (!string.IsNullOrEmpty(expectedErrorContains))
        {
            Assert.Contains(expectedErrorContains, result.ErrorMessage);
        }
    }

    public static void ShouldBeEqual(double expected, double actual, double tolerance = 0.0001)
    {
        Assert.True(Math.Abs(expected - actual) < tolerance, 
            $"Expected {expected}, but got {actual}");
    }
}