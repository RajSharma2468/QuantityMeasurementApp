using NUnit.Framework;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    public class InchesTest
    {
        [Test]
        public void GivenTwoEqualInches_ShouldReturnTrue()
        {
            Inches inch1 = new Inches(5);
            Inches inch2 = new Inches(5);

            Assert.That(inch1.Equals(inch2), Is.True);
        }

        [Test]
        public void GivenTwoDifferentInches_ShouldReturnFalse()
        {
            Inches inch1 = new Inches(5);
            Inches inch2 = new Inches(7);

            Assert.That(inch1.Equals(inch2), Is.False);
        }
    }
}