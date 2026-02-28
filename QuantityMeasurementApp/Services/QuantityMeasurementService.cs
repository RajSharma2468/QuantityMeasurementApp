using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public QuantityLength Add(QuantityLength q1, QuantityLength q2, LengthUnit targetUnit)
        {
            if (q1 == null) throw new ArgumentNullException(nameof(q1));
            if (q2 == null) throw new ArgumentNullException(nameof(q2));

            return q1.Add(q2, targetUnit);
        }
    }
}