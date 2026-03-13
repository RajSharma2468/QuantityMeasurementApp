using BusinessLayer.Interfaces;
using ModelLayer.Enums;
using ModelLayer.Exceptions;
using ModelLayer.Interfaces;
using ModelLayer.Models;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class QuantityMeasurementService : IQuantityMeasurementService
    {
        private readonly IQuantityRepository _repository;

        public QuantityMeasurementService(IQuantityRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public QuantityDTO Compare(QuantityDTO quantity1, QuantityDTO quantity2)
        {
            try
            {
                ValidateQuantities(quantity1, quantity2);

                var model1 = CreateQuantityModel(quantity1);
                var model2 = CreateQuantityModel(quantity2);

                ValidateCompatibleTypes(model1, model2);

                bool areEqual = AreQuantitiesEqual(model1, model2);

                // Save to repository
                var entity = new QuantityMeasurementEntity(
                    "COMPARE",
                    quantity1.Value,
                    quantity1.UnitName,
                    quantity2.Value,
                    quantity2.UnitName,
                    areEqual ? 1.0 : 0.0,
                    "boolean"
                );
                _repository.Save(entity);

                return new QuantityDTO
                {
                    Value = areEqual ? 1 : 0,
                    UnitName = "boolean",
                    MeasurementType = "Comparison"
                };
            }
            catch (Exception ex)
            {
                HandleAndSaveError("COMPARE", quantity1, quantity2, ex);
                throw new QuantityMeasurementException("Error during comparison", ex);
            }
        }

        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
        {
            try
            {
                if (quantity == null)
                    throw new ArgumentNullException(nameof(quantity));

                if (string.IsNullOrWhiteSpace(targetUnit))
                    throw new ArgumentException("Target unit cannot be null or empty");

                var sourceModel = CreateQuantityModel(quantity);
                var targetMeasurable = GetUnitFromName(sourceModel.Unit.GetMeasurementType(), targetUnit);

                if (targetMeasurable == null)
                    throw new ArgumentException($"Invalid target unit: {targetUnit}");

                double result = sourceModel.ConvertTo((dynamic)targetMeasurable);

                // Save to repository
                var entity = new QuantityMeasurementEntity(
                    "CONVERT",
                    quantity.Value,
                    quantity.UnitName,
                    result,
                    targetUnit
                );
                _repository.Save(entity);

                return new QuantityDTO
                {
                    Value = result,
                    UnitName = targetUnit,
                    MeasurementType = sourceModel.Unit.GetMeasurementType()
                };
            }
            catch (Exception ex)
            {
                HandleAndSaveError("CONVERT", quantity, null, ex);
                throw new QuantityMeasurementException("Error during conversion", ex);
            }
        }

        public QuantityDTO Add(QuantityDTO quantity1, QuantityDTO quantity2)
        {
            try
            {
                ValidateQuantities(quantity1, quantity2);

                var model1 = CreateQuantityModel(quantity1);
                var model2 = CreateQuantityModel(quantity2);

                ValidateCompatibleTypes(model1, model2);

                var result = AddQuantities(model1, model2);

                // Save to repository
                var entity = new QuantityMeasurementEntity(
                    "ADD",
                    quantity1.Value,
                    quantity1.UnitName,
                    quantity2.Value,
                    quantity2.UnitName,
                    result.Value,
                    result.Unit.GetUnitName()
                );
                _repository.Save(entity);

                return new QuantityDTO
                {
                    Value = result.Value,
                    UnitName = result.Unit.GetUnitName(),
                    MeasurementType = result.Unit.GetMeasurementType()
                };
            }
            catch (Exception ex)
            {
                HandleAndSaveError("ADD", quantity1, quantity2, ex);
                throw new QuantityMeasurementException("Error during addition", ex);
            }
        }

        public QuantityDTO Subtract(QuantityDTO quantity1, QuantityDTO quantity2)
        {
            try
            {
                ValidateQuantities(quantity1, quantity2);

                var model1 = CreateQuantityModel(quantity1);
                var model2 = CreateQuantityModel(quantity2);

                ValidateCompatibleTypes(model1, model2);

                var result = SubtractQuantities(model1, model2);

                // Save to repository
                var entity = new QuantityMeasurementEntity(
                    "SUBTRACT",
                    quantity1.Value,
                    quantity1.UnitName,
                    quantity2.Value,
                    quantity2.UnitName,
                    result.Value,
                    result.Unit.GetUnitName()
                );
                _repository.Save(entity);

                return new QuantityDTO
                {
                    Value = result.Value,
                    UnitName = result.Unit.GetUnitName(),
                    MeasurementType = result.Unit.GetMeasurementType()
                };
            }
            catch (Exception ex)
            {
                HandleAndSaveError("SUBTRACT", quantity1, quantity2, ex);
                throw new QuantityMeasurementException("Error during subtraction", ex);
            }
        }

        public QuantityDTO Divide(QuantityDTO quantity1, QuantityDTO quantity2)
        {
            try
            {
                ValidateQuantities(quantity1, quantity2);

                var model1 = CreateQuantityModel(quantity1);
                var model2 = CreateQuantityModel(quantity2);

                ValidateCompatibleTypes(model1, model2);

                double result = DivideQuantities(model1, model2);

                // Save to repository
                var entity = new QuantityMeasurementEntity(
                    "DIVIDE",
                    quantity1.Value,
                    quantity1.UnitName,
                    quantity2.Value,
                    quantity2.UnitName,
                    result,
                    null
                );
                _repository.Save(entity);

                return new QuantityDTO
                {
                    Value = result,
                    UnitName = "ratio",
                    MeasurementType = "Dimensionless"
                };
            }
            catch (Exception ex)
            {
                HandleAndSaveError("DIVIDE", quantity1, quantity2, ex);
                throw new QuantityMeasurementException("Error during division", ex);
            }
        }

        public List<QuantityMeasurementEntity> GetOperationHistory()
        {
            return _repository.GetAll();
        }

        #region Private Helper Methods

        private void ValidateQuantities(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null)
                throw new ArgumentNullException(nameof(q1));
            if (q2 == null)
                throw new ArgumentNullException(nameof(q2));
        }

        private dynamic CreateQuantityModel(QuantityDTO dto)
        {
            var unit = GetUnitFromName(dto.MeasurementType, dto.UnitName);

            switch (dto.MeasurementType.ToLower())
            {
                case "length":
                    return new QuantityModel<LengthUnit>(dto.Value, (LengthUnit)unit);
                case "weight":
                    return new QuantityModel<WeightUnit>(dto.Value, (WeightUnit)unit);
                case "volume":
                    return new QuantityModel<VolumeUnit>(dto.Value, (VolumeUnit)unit);
                case "temperature":
                    return new QuantityModel<TemperatureUnit>(dto.Value, (TemperatureUnit)unit);
                default:
                    throw new ArgumentException($"Unsupported measurement type: {dto.MeasurementType}");
            }
        }

        private IMeasurable GetUnitFromName(string measurementType, string unitName)
        {
            switch (measurementType.ToLower())
            {
                case "length":
                    if (Enum.TryParse<LengthUnitType>(unitName, true, out var lengthUnit))
                        return new LengthUnit(lengthUnit);
                    break;
                case "weight":
                    if (Enum.TryParse<WeightUnitType>(unitName, true, out var weightUnit))
                        return new WeightUnit(weightUnit);
                    break;
                case "volume":
                    if (Enum.TryParse<VolumeUnitType>(unitName, true, out var volumeUnit))
                        return new VolumeUnit(volumeUnit);
                    break;
                case "temperature":
                    if (Enum.TryParse<TemperatureUnitType>(unitName, true, out var tempUnit))
                        return new TemperatureUnit(tempUnit);
                    break;
            }
            throw new ArgumentException($"Invalid unit: {unitName} for measurement type: {measurementType}");
        }

        private void ValidateCompatibleTypes(dynamic model1, dynamic model2)
        {
            string type1 = model1.Unit.GetMeasurementType();
            string type2 = model2.Unit.GetMeasurementType();

            if (!type1.Equals(type2))
            {
                throw new InvalidOperationException($"Cannot operate on different measurement types: {type1} and {type2}");
            }
        }

        private bool AreQuantitiesEqual(dynamic model1, dynamic model2)
        {
            return model1.Equals(model2);
        }

        private dynamic AddQuantities(dynamic model1, dynamic model2)
        {
            return model1.Add(model2);
        }

        private dynamic SubtractQuantities(dynamic model1, dynamic model2)
        {
            return model1.Subtract(model2);
        }

        private double DivideQuantities(dynamic model1, dynamic model2)
        {
            return model1.Divide(model2);
        }

        private void HandleAndSaveError(string operation, QuantityDTO q1, QuantityDTO q2, Exception ex)
        {
            var entity = new QuantityMeasurementEntity(
                operation,
                q1?.Value,
                q1?.UnitName,
                q2?.Value,
                q2?.UnitName,
                ex.Message
            );
            _repository.Save(entity);
        }

        #endregion
    }
}