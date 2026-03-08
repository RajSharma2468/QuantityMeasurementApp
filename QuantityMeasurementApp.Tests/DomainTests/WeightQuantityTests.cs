using NUnit.Framework;
using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Units;

namespace QuantityMeasurementApp.Tests.DomainTests
{
    public class WeightQuantityTests
    {
        [Test]
        public void Kilogram_Equals_Gram()
        {
            QuantityWeight q1 = new QuantityWeight(1, WeightUnit.KILOGRAM);
            QuantityWeight q2 = new QuantityWeight(1000, WeightUnit.GRAM);

            Assert.IsTrue(q1.Equals(q2));
        }

        [Test]
        public void Pound_To_Kilogram()
        {
            QuantityWeight q = new QuantityWeight(2.20462, WeightUnit.POUND);

            QuantityWeight result = q.ConvertTo(WeightUnit.KILOGRAM);

            Assert.AreEqual(1, result.ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM).ConvertTo(WeightUnit.KILOGRAM),1,0.01);
        }
    }
}