using Xunit;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;
using QuantityMeasurementApp.Core.Exceptions;

namespace QuantityMeasurementApp.Tests.DomainTests.GenericQuantityTests
{
    public class GenericQuantityEdgeCasesTests
    {
        [Fact]
        public void Constructor_NaNValue_ShouldThrowInvalidValueException()
        {
            // Act & Assert
            Assert.Throws<InvalidValueException>(() => 
                new GenericQuantity<LengthUnit>(double.NaN, LengthUnit.Feet));
        }

        [Fact]
        public void Constructor_InfinityValue_ShouldThrowInvalidValueException()
        {
            // Act & Assert
            Assert.Throws<InvalidValueException>(() => 
                new GenericQuantity<LengthUnit>(double.PositiveInfinity, LengthUnit.Feet));
        }

        [Fact]
        public void Constructor_NullUnit_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                new GenericQuantity<LengthUnit>(10, null!));
        }

        [Fact]
        public void ConvertTo_NullTargetUnit_ShouldThrowArgumentNullException()
        {
            // Arrange
            var quantity = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                quantity.ConvertTo(null!));
        }

        [Fact]
        public void Add_NullOther_ShouldThrowArgumentNullException()
        {
            // Arrange
            var quantity = new GenericQuantity<LengthUnit>(10, LengthUnit.Feet);
            
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => 
                quantity.Add(null!));
        }

        [Fact]
        public void VerySmallValues_ShouldMaintainPrecision()
        {
            // Arrange
            var verySmall = 1e-10;
            var q1 = new GenericQuantity<WeightUnit>(verySmall, WeightUnit.Kilogram);
            var q2 = new GenericQuantity<WeightUnit>(verySmall * 1000, WeightUnit.Gram);
            
            // Act & Assert
            Assert.True(q1.Equals(q2));
        }

        [Fact]
        public void HashCode_ConsistentWithEquals()
        {
            // Arrange
            var q1 = new GenericQuantity<LengthUnit>(1, LengthUnit.Feet);
            var q2 = new GenericQuantity<LengthUnit>(12, LengthUnit.Inch);
            
            // Act & Assert
            Assert.Equal(q1.GetHashCode(), q2.GetHashCode());
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            var q = new GenericQuantity<WeightUnit>(1.5, WeightUnit.Kilogram);
            
            // Act
            var result = q.ToString();
            
            // Assert
            Assert.Equal("1.50 kg", result);
        }
    }
}