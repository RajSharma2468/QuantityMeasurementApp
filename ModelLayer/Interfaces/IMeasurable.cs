using System;

namespace ModelLayer.Interfaces
{
    public interface IMeasurable
    {
        /// <summary>
        /// Converts the given value to the base unit
        /// </summary>
        double ToBaseUnit(double value);

        /// <summary>
        /// Converts from base unit to this unit
        /// </summary>
        double FromBaseUnit(double value);

        /// <summary>
        /// Gets the name of the unit
        /// </summary>
        string GetUnitName();

        /// <summary>
        /// Gets the measurement type (Length, Weight, Volume, Temperature)
        /// </summary>
        string GetMeasurementType();

        /// <summary>
        /// Gets the unit instance from unit name
        /// </summary>
        IMeasurable GetUnitFromName(string unitName);
    }
}