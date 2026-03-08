using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests
{
    public class GenericQuantityArithmeticTests
    {
        private const double Epsilon = 1e-10;

        [Fact]
        public void Add_SameUnit_ShouldSumCorrectly()
        {
            // Arrange
            var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new GenericQuantity<LengthUnit>(3, LengthUnit.Feet);
            
            // Act
            var result = q1.Add(q2);
            
            // Assert
            Assert.Equal(8, result.Value, Epsilon);
            Assert.Equal(LengthUnit.Feet.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        }

        [Fact]
        public void Add_DifferentUnits_ShouldSumInFirstUnit()
        {
            // Arrange
            var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
            
            // Act
            var result = q1.Add(q2);
            
            // Assert
            Assert.Equal(2, result.Value, Epsilon);
            Assert.Equal(LengthUnit.Feet.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        }

        [Fact]
        public void Add_WithTargetUnit_ShouldReturnInTargetUnit()
        {
            // Arrange
            var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new GenericQuantity<LengthUnit>(1, LengthUnit.Yard);
            
            // Act
            var result = q1.Add(q2, LengthUnit.Inch);
            
            // Assert
            Assert.Equal(48, result.Value, Epsilon);
            Assert.Equal(LengthUnit.Inch.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        }

        [Fact]
        public void Add_WeightDifferentUnits_ShouldSumCorrectly()
        {
            // Arrange
            var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
            
            // Act
            var result = w1.Add(w2);
            
            // Assert
            Assert.Equal(1.5, result.Value, Epsilon);
            Assert.Equal(WeightUnit.Kilogram.GetUnitSymbol(), result.Unit.GetUnitSymbol());
        }

        [Fact]
        public void Add_WeightWithTargetUnit_ShouldReturnInTargetUnit()
        {
            // Arrange
            var w1 = new GenericQuantity<WeightUnit>(1, WeightUnit.Kilogram);
            var w2 = new GenericQuantity<WeightUnit>(500, WeightUnit.Gram);
            
            // Act
            var result = w1.Add(w2, WeightUnit.Pound);
            
            // Assert
            Assert.Equal(WeightUnit.Pound.GetUnitSymbol(), result.Unit.GetUnitSymbol());
            Assert.True(result.Value > 0);
        }

        [Fact]
        public void Add_WithZero_ShouldReturnOriginal()
        {
            // Arrange
            var q1 = new GenericQuantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new GenericQuantity<LengthUnit>(0, LengthUnit.Inch);
            
            // Act
            var result = q1.Add(q2);
            
            // Assert
            Assert.Equal(5, result.Value, Epsilon);
        }

        [Fact]
        public void Add_NegativeValues_ShouldSubtract()
        {
            // Arrange
            var q1 = new GenericQuantity<WeightUnit>(5, WeightUnit.Kilogram);
            var q2 = new GenericQuantity<WeightUnit>(-2000, WeightUnit.Gram);
            
            // Act
            var result = q1.Add(q2);
            
            // Assert
            Assert.Equal(3, result.Value, Epsilon);
        }
    }
}