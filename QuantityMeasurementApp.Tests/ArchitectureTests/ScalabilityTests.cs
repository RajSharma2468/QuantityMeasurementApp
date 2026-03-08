using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.ArchitectureTests;

public class ScalabilityTests
{
    [Fact]
    public void MultipleCategories_WeightAndLength_ShouldBeIndependent()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var length = new QuantityLength(1.0, LengthUnit.FEET);
        
        // Act & Assert - They should not be equal
        Assert.False(weight.Equals(length));
        
        // Their hash codes should be different
        Assert.NotEqual(weight.GetHashCode(), length.GetHashCode());
    }

    [Fact]
    public void Pattern_WeightMirrorsLength_ShouldHaveSimilarStructure()
    {
        // Arrange
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var length = new QuantityLength(1.0, LengthUnit.FEET);
        
        // Assert - Both should have Value and Unit properties
        Assert.IsType<double>(weight.Value);
        Assert.IsType<double>(length.Value);
        
        Assert.IsType<WeightUnit>(weight.Unit);
        Assert.IsType<LengthUnit>(length.Unit);
    }

    [Fact]
    public void BothCategories_SupportSameOperations_ShouldWorkIndependently()
    {
        // Arrange - Weight operations
        var weight1 = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weight2 = new QuantityWeight(1000.0, WeightUnit.GRAM);
        
        // Arrange - Length operations
        var length1 = new QuantityLength(1.0, LengthUnit.FEET);
        var length2 = new QuantityLength(12.0, LengthUnit.INCH);
        
        // Act - Weight comparison and conversion
        var weightEqual = weight1.Equals(weight2);
        var weightConverted = weight1.ConvertTo(WeightUnit.POUND);
        
        // Act - Length comparison and conversion
        var lengthEqual = length1.Equals(length2);
        var lengthConverted = length1.ConvertTo(LengthUnit.YARD);
        
        // Assert - Both should work correctly
        Assert.True(weightEqual);
        Assert.True(lengthEqual);
        
        Assert.IsType<QuantityWeight>(weightConverted);
        Assert.IsType<QuantityLength>(lengthConverted);
    }

    [Fact]
    public void NewCategory_CanBeAdded_WithoutAffectingExisting()
    {
        // This test demonstrates that the pattern allows adding new categories
        // without affecting existing ones
        
        // Arrange - Existing length functionality
        var length = new QuantityLength(1.0, LengthUnit.FEET);
        var lengthInInches = length.ConvertTo(LengthUnit.INCH);
        
        // Arrange - New weight functionality (added in UC9)
        var weight = new QuantityWeight(1.0, WeightUnit.KILOGRAM);
        var weightInGrams = weight.ConvertTo(WeightUnit.GRAM);
        
        // Assert - Both work independently
        Assert.Equal(12.0, lengthInInches.Value);
        Assert.Equal(1000.0, weightInGrams.Value);
        
        // They shouldn't interfere with each other
        Assert.False(length.Equals(weight));
    }
}