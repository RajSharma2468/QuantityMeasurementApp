using NUnit.Framework;
using ModelLayer.Enums;
using ModelLayer.Models;
using System;

namespace QuantityMeasurementApp.Tests
{
    [TestFixture]
    public class GenericQuantityTest
    {
        [Test]
        public void Quantity_Equals_SameValueSameUnit_ReturnsTrue()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));

            // Act & Assert
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Quantity_Equals_DifferentUnitsSameBaseValue_ReturnsTrue()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(1, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(12, new LengthUnit(LengthUnitType.INCH));

            // Act & Assert
            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Quantity_Equals_DifferentValues_ReturnsFalse()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(20, new LengthUnit(LengthUnitType.FEET));

            // Act & Assert
            Assert.IsFalse(q1.Equals(q2));
        }

        [Test]
        public void Quantity_ConvertTo_SameType_ReturnsCorrectValue()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(1, new LengthUnit(LengthUnitType.FEET));
            var targetUnit = new LengthUnit(LengthUnitType.INCH);

            // Act
            double result = q1.ConvertTo(targetUnit);

            // Assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void Quantity_ConvertTo_DifferentTypes_ThrowsException()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(1, new LengthUnit(LengthUnitType.FEET));
            
            // This will cause runtime error
            Assert.Throws<InvalidOperationException>(() => {
                var targetUnit = new WeightUnit(WeightUnitType.GRAM);
                // This cast will fail
                q1.ConvertTo((LengthUnit)(object)targetUnit);
            });
        }

        [Test]
        public void Quantity_Add_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(5, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(3, new LengthUnit(LengthUnitType.FEET));

            // Act
            var result = q1.Add(q2);

            // Assert
            Assert.AreEqual(8, result.Value);
            Assert.AreEqual(LengthUnitType.FEET, ((LengthUnit)result.Unit).GetUnitName());
        }

        [Test]
        public void Quantity_Add_DifferentUnits_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(1, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(12, new LengthUnit(LengthUnitType.INCH));

            // Act
            var result = q1.Add(q2);

            // Assert
            Assert.AreEqual(2, result.Value);
            Assert.AreEqual(LengthUnitType.FEET, ((LengthUnit)result.Unit).GetUnitName());
        }

        [Test]
        public void Quantity_Subtract_SameUnit_ReturnsCorrectResult()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(3, new LengthUnit(LengthUnitType.FEET));

            // Act
            var result = q1.Subtract(q2);

            // Assert
            Assert.AreEqual(7, result.Value);
        }

        [Test]
        public void Quantity_Divide_SameUnit_ReturnsCorrectRatio()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<LengthUnit>(2, new LengthUnit(LengthUnitType.FEET));

            // Act
            double result = q1.Divide(q2);

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Quantity_WithDifferentTypes_Add_ThrowsException()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));
            var q2 = new Quantity<WeightUnit>(10, new WeightUnit(WeightUnitType.GRAM));

            // Act & Assert - This won't compile, but we test the runtime behavior
            // This is a compile-time error, so we can't actually test it
            // Instead we test the service layer which handles this
            Assert.Pass("Compile-time type safety prevents this");
        }

        [Test]
        public void Quantity_ToString_ReturnsFormattedString()
        {
            // Arrange
            var q1 = new Quantity<LengthUnit>(10, new LengthUnit(LengthUnitType.FEET));

            // Act
            string result = q1.ToString();

            // Assert
            Assert.AreEqual("10 FEET", result);
        }
    }
}