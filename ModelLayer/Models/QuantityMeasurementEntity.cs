using System;
using System.Text;

namespace ModelLayer.Models
{
    [Serializable]
    public class QuantityMeasurementEntity
    {
        public DateTime Timestamp { get; }
        public string OperationType { get; }
        public double? Operand1Value { get; }
        public string Operand1Unit { get; }
        public double? Operand2Value { get; }
        public string Operand2Unit { get; }
        public double? ResultValue { get; }
        public string ResultUnit { get; }
        public bool HasError { get; }
        public string ErrorMessage { get; }

        // Constructor for single operand operations (conversion)
        public QuantityMeasurementEntity(
            string operationType,
            double operandValue,
            string operandUnit,
            double resultValue,
            string resultUnit)
        {
            Timestamp = DateTime.Now;
            OperationType = operationType;
            Operand1Value = operandValue;
            Operand1Unit = operandUnit;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
            HasError = false;
        }

        // Constructor for binary operations (comparison, addition, subtraction, division)
        public QuantityMeasurementEntity(
            string operationType,
            double operand1Value,
            string operand1Unit,
            double operand2Value,
            string operand2Unit,
            double? resultValue,
            string resultUnit = null)
        {
            Timestamp = DateTime.Now;
            OperationType = operationType;
            Operand1Value = operand1Value;
            Operand1Unit = operand1Unit;
            Operand2Value = operand2Value;
            Operand2Unit = operand2Unit;
            ResultValue = resultValue;
            ResultUnit = resultUnit;
            HasError = false;
        }

        // Constructor for errors
        public QuantityMeasurementEntity(
            string operationType,
            double? operand1Value,
            string operand1Unit,
            double? operand2Value,
            string operand2Unit,
            string errorMessage)
        {
            Timestamp = DateTime.Now;
            OperationType = operationType;
            Operand1Value = operand1Value;
            Operand1Unit = operand1Unit;
            Operand2Value = operand2Value;
            Operand2Unit = operand2Unit;
            ErrorMessage = errorMessage;
            HasError = true;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Operation: {OperationType}");

            if (Operand1Value.HasValue)
                sb.AppendLine($"Operand 1: {Operand1Value} {Operand1Unit}");

            if (Operand2Value.HasValue)
                sb.AppendLine($"Operand 2: {Operand2Value} {Operand2Unit}");

            if (HasError)
            {
                sb.AppendLine($"Error: {ErrorMessage}");
            }
            else if (ResultValue.HasValue)
            {
                if (!string.IsNullOrEmpty(ResultUnit))
                    sb.AppendLine($"Result: {ResultValue} {ResultUnit}");
                else
                    sb.AppendLine($"Result: {ResultValue}");
            }

            return sb.ToString();
        }
    }
}