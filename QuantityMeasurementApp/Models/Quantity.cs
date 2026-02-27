using System;

namespace QuantityMeasurementApp.Models
{
    public class Quantity
    {
        private const double EPSILON = 0.0001;

        public double Value { get; }
        public LengthUnit Unit { get; }

        public Quantity(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid value");

            Value = value;
            Unit = unit;
        }

        public double ToFeet()
        {
            switch (Unit)
            {
                case LengthUnit.FEET: return Value;
                case LengthUnit.INCH: return Value / 12;
                case LengthUnit.YARD: return Value * 3;
                case LengthUnit.CENTIMETER: return Value * 0.0328084;
                default: throw new ArgumentException("Invalid unit");
            }
        }

        public Quantity ConvertTo(LengthUnit target)
        {
            double valueInFeet = ToFeet();
            double result;

            switch (target)
            {
                case LengthUnit.FEET: result = valueInFeet; break;
                case LengthUnit.INCH: result = valueInFeet * 12; break;
                case LengthUnit.YARD: result = valueInFeet / 3; break;
                case LengthUnit.CENTIMETER: result = valueInFeet / 0.0328084; break;
                default: throw new ArgumentException("Invalid unit");
            }

            return new Quantity(result, target);
        }

        //  FIXED NULLABILITY SIGNATURE
        public override bool Equals(object? obj)
        {
            if (obj is not Quantity other)
                return false;

            return Math.Abs(this.ToFeet() - other.ToFeet()) < EPSILON;
        }

        public override int GetHashCode()
        {
            return ToFeet().GetHashCode();
        }
    }
}