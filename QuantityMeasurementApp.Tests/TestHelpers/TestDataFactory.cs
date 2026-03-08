using QuantityMeasurementApp.Core.Domain.Quantities;
using QuantityMeasurementApp.Core.Units;

namespace QuantityMeasurementApp.Tests.TestHelpers
{
    public static class TestDataFactory
    {
        public static QuantityLength OneFoot()
        {
            return new QuantityLength(1, LengthUnit.FOOT);
        }

        public static QuantityLength TwelveInch()
        {
            return new QuantityLength(12, LengthUnit.INCH);
        }
    }
}