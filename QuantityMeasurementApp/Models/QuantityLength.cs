using System;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Models
{
    public class QuantityLength
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        // Conversion factor to base unit (feet)
        private static readonly Dictionary<LengthUnit, double> ConversionToFeet = new()
        {
            { LengthUnit.FEET, 1.0 },
            { LengthUnit.INCHES, 1.0 / 12.0 },
            { LengthUnit.YARDS, 3.0 },
            { LengthUnit.CENTIMETERS, 1.0 / 30.48 }
        };

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit");
            Value = value;
            Unit = unit;
        }

        // UC7 Add method: returns new QuantityLength in target unit
        public QuantityLength Add(QuantityLength other, LengthUnit targetUnit)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (!Enum.IsDefined(typeof(LengthUnit), targetUnit))
                throw new ArgumentException("Invalid target unit");

            double thisInFeet = Value * ConversionToFeet[Unit];
            double otherInFeet = other.Value * ConversionToFeet[other.Unit];

            double sumInFeet = thisInFeet + otherInFeet;

            double resultValue = sumInFeet / ConversionToFeet[targetUnit];
            resultValue = Math.Round(resultValue, 3);

            return new QuantityLength(resultValue, targetUnit);
        }
    }
}