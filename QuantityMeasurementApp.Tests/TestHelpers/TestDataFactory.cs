using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Domain.Units;

namespace QuantityMeasurementApp.Tests.TestHelpers
{
    public static class TestDataFactory
    {
        public static Quantity OneFoot()
        {
            return new Quantity(1, LengthUnit.FEET);
        }

        public static Quantity TwelveInches()
        {
            return new Quantity(12, LengthUnit.INCHES);
        }

        public static Quantity OneYard()
        {
            return new Quantity(1, LengthUnit.YARDS);
        }
    }
}