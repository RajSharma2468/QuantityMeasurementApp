using NUnit.Framework;
using QuantityMeasurementApp.Models;
using System;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityConversionEdgeCasesTests
    {
        [Test]
        public void ZeroValue_ShouldConvertToZero()
        {
            Quantity q = new Quantity(0, LengthUnit.FEET);
            Quantity result = q.ConvertTo(LengthUnit.INCH);

            Assert.That(result.Value, Is.EqualTo(0));
        }

        [Test]
        public void NegativeValue_ShouldConvertCorrectly()
        {
            Quantity q = new Quantity(-1, LengthUnit.FEET);
            Quantity result = q.ConvertTo(LengthUnit.INCH);

            Assert.That(result.Value, Is.EqualTo(-12));
        }

        [Test]
        public void NaN_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity(double.NaN, LengthUnit.FEET));
        }

        [Test]
        public void Infinity_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Quantity(double.PositiveInfinity, LengthUnit.FEET));
        }
    }
}