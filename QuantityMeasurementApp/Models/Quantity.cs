using System;

namespace QuantityMeasurementApp.Models
{
    public class Quantity
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        private const double INCH_TO_FEET = 1.0 / 12.0;
        private const double YARD_TO_FEET = 3.0;
        private const double METER_TO_FEET = 3.28084;

        public Quantity(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value");

            if (!Enum.IsDefined(typeof(LengthUnit), unit))
                throw new ArgumentException("Invalid unit");

            Value = value;
            Unit = unit;
        }

        private double ToFeet()
        {
            switch (Unit)
            {
                case LengthUnit.FEET:
                    return Value;

                case LengthUnit.INCHES:
                    return Value * INCH_TO_FEET;

                case LengthUnit.YARDS:
                    return Value * YARD_TO_FEET;

                case LengthUnit.METER:
                    return Value * METER_TO_FEET;

                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

        private static double FromFeet(double feet, LengthUnit targetUnit)
        {
            switch (targetUnit)
            {
                case LengthUnit.FEET:
                    return feet;

                case LengthUnit.INCHES:
                    return feet * 12;

                case LengthUnit.YARDS:
                    return feet / 3;

                case LengthUnit.METER:
                    return feet / 3.28084;

                default:
                    throw new ArgumentException("Unsupported unit");
            }
        }

        // UC6 Addition
        public Quantity Add(Quantity other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            double sumFeet = this.ToFeet() + other.ToFeet();
            double resultValue = FromFeet(sumFeet, this.Unit);

            return new Quantity(resultValue, this.Unit);
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}