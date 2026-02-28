using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class QuantityMeasurementService
    {
        public Quantity Add(Quantity q1, Quantity q2)
        {
            return q1.Add(q2);
        }
    }
}