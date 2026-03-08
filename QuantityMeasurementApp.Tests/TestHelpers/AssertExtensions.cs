using Xunit;

namespace QuantityMeasurementApp.Tests.TestHelpers;

public static class AssertExtensions
{
    public static void ApproximatelyEqual(double expected, double actual, double tolerance = 1e-10)
    {
        Assert.True(Math.Abs(expected - actual) < tolerance, 
            $"Expected: {expected}, Actual: {actual}, Difference: {Math.Abs(expected - actual)}");
    }

    public static void CollectionNotEmpty<T>(IEnumerable<T> collection, string message = "Collection should not be empty")
    {
        Assert.True(collection.Any(), message);
    }

    public static void NotNullWithValue<T>(T obj, string message = "Object should not be null") where T : class
    {
        Assert.NotNull(obj);
    }
}