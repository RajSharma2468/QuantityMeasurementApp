using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    // Service class that contains all business logic
    public class QuantityMeasurementService
    {
        // Method to compare two Feet objects
        public bool CompareFeet(Feet f1, Feet f2)
        {
            // Use Equals method to compare
            return f1.Equals(f2);
        }

        // Method to add two feet values
        public double AddFeet(Feet f1, Feet f2)
        {
            // Return sum of both feet values
            return f1.Value + f2.Value;
        }

        // Method to convert feet to inches
        public double ConvertFeetToInches(Feet feet)
        {
            // Call conversion method from model
            return feet.ToInches();
        }
    }
}