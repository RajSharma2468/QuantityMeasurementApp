using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests;

public class BasicTests
{
    [Fact]
    public void Test_XUnit_Is_Working()
    {
        // Arrange
        var quantity = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
        
        // Act & Assert
        Assert.NotNull(quantity);
        Assert.Equal(10, quantity.Value);
        Assert.Equal(LengthUnit.Feet.GetUnitSymbol(), quantity.Unit.GetUnitSymbol());
    }
}
