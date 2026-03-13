using ModelLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        /// <summary>
        /// Compares two quantities for equality
        /// </summary>
        QuantityDTO Compare(QuantityDTO quantity1, QuantityDTO quantity2);

        /// <summary>
        /// Converts a quantity to a different unit
        /// </summary>
        QuantityDTO Convert(QuantityDTO quantity, string targetUnit);

        /// <summary>
        /// Adds two quantities
        /// </summary>
        QuantityDTO Add(QuantityDTO quantity1, QuantityDTO quantity2);

        /// <summary>
        /// Subtracts one quantity from another
        /// </summary>
        QuantityDTO Subtract(QuantityDTO quantity1, QuantityDTO quantity2);

        /// <summary>
        /// Divides one quantity by another
        /// </summary>
        QuantityDTO Divide(QuantityDTO quantity1, QuantityDTO quantity2);

        /// <summary>
        /// Gets the operation history from the repository
        /// </summary>
        List<QuantityMeasurementEntity> GetOperationHistory();
    }
}