using System.Collections.Generic;
using QuantityMeasurementModelLayer.DTO;
using QuantityMeasurementModelLayer.Models;

namespace QuantityMeasurementBusinessLayer.Interfaces
{
    public interface IQuantityMeasurementService
    {
        QuantityDTO Add(QuantityModel q1, QuantityModel q2);
        QuantityDTO Subtract(QuantityModel q1, QuantityModel q2);
        bool Compare(QuantityModel q1, QuantityModel q2);
        QuantityDTO Convert(QuantityModel from, QuantityModel to);
        List<QuantityDTO> GetAllMeasurements();
        List<QuantityDTO> GetMeasurementsByOperation(string operation);
        List<QuantityDTO> GetMeasurementsByUnitType(string unitType);
        int GetTotalCount();
        void DeleteAll();
    }
}