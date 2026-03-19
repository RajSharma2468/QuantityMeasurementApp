using QuantityMeasurementBusinessLayer.DTOs;
using QuantityMeasurementModelLayer.Entities;

namespace QuantityMeasurementApp.Tests.Helpers;

public static class TestDataHelper
{
    public static QuantityInputDTO GetValidCompareInput()
    {
        return new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
    }

    public static QuantityInputDTO GetValidConvertInput()
    {
        return new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 0, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
    }

    public static QuantityInputDTO GetValidAddInput()
    {
        return new QuantityInputDTO
        {
            ThisQuantity = new QuantityDTO { Value = 1, Unit = "Feet", MeasurementType = "LengthUnit" },
            ThatQuantity = new QuantityDTO { Value = 12, Unit = "Inches", MeasurementType = "LengthUnit" }
        };
    }

    public static QuantityMeasurement GetSampleEntity()
    {
        return new QuantityMeasurement
        {
            ThisValue = 1,
            ThisUnit = "Feet",
            ThisMeasurementType = "LengthUnit",
            ThatValue = 12,
            ThatUnit = "Inches",
            ThatMeasurementType = "LengthUnit",
            Operation = "compare",
            ResultString = "true",
            IsError = false
        };
    }

    public static List<QuantityMeasurement> GetSampleEntityList()
    {
        return new List<QuantityMeasurement>
        {
            new QuantityMeasurement { Operation = "compare", ThisValue = 1 },
            new QuantityMeasurement { Operation = "convert", ResultValue = 12 },
            new QuantityMeasurement { Operation = "add", ResultValue = 2 }
        };
    }

    public static QuantityResultDTO GetSampleResult()
    {
        return new QuantityResultDTO
        {
            ThisValue = 1,
            ThisUnit = "Feet",
            ThisMeasurementType = "LengthUnit",
            ThatValue = 12,
            ThatUnit = "Inches",
            ThatMeasurementType = "LengthUnit",
            Operation = "compare",
            ResultString = "true",
            IsError = false,
            CreatedAt = DateTime.UtcNow
        };
    }
}