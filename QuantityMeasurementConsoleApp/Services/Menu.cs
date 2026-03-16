using System;
using System.Collections.Generic;
using QuantityMeasurementConsoleApp.Interfaces;
using QuantityMeasurementBusinessLayer.Interfaces;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.DTO;
using Microsoft.Extensions.Logging;

namespace QuantityMeasurementConsoleApp.Services
{
    public class Menu : IMenu
    {
        private IQuantityMeasurementService _service;
        private ILogger<Menu> _logger;
        
        public Menu(IQuantityMeasurementService service, ILogger<Menu> logger)
        {
            this._service = service;
            this._logger = logger;
        }
        
        public void ShowMainMenu()
        {
            bool exit = false;
            
            while (!exit)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine("QUANTITY MEASUREMENT APPLICATION");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Volume Operations");
                Console.WriteLine("4. Temperature Operations");
                Console.WriteLine("5. View History");
                Console.WriteLine("6. Exit");
                Console.WriteLine("========================================");
                Console.Write("Enter your choice: ");
                
                string choice = Console.ReadLine();
                
                if (choice == "1")
                {
                    this.ShowLengthMenu();
                }
                else if (choice == "2")
                {
                    this.ShowWeightMenu();
                }
                else if (choice == "3")
                {
                    this.ShowVolumeMenu();
                }
                else if (choice == "4")
                {
                    this.ShowTemperatureMenu();
                }
                else if (choice == "5")
                {
                    this.ShowHistoryMenu();
                }
                else if (choice == "6")
                {
                    exit = true;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }
        
        public void ShowLengthMenu()
        {
            Console.WriteLine("\n--- LENGTH OPERATIONS ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Compare");
            Console.WriteLine("4. Convert");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                this.PerformLengthOperation("Add");
            }
            else if (choice == "2")
            {
                this.PerformLengthOperation("Subtract");
            }
            else if (choice == "3")
            {
                this.PerformLengthCompare();
            }
            else if (choice == "4")
            {
                this.PerformLengthConvert();
            }
        }
        
        private void PerformLengthOperation(string operation)
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowLengthUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                LengthUnit unit1 = (LengthUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowLengthUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                LengthUnit unit2 = (LengthUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityLength(val1, unit1);
                QuantityModel q2 = new QuantityLength(val2, unit2);
                
                QuantityDTO result = null;
                
                if (operation == "Add")
                {
                    result = this._service.Add(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " + " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
                else if (operation == "Subtract")
                {
                    result = this._service.Subtract(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " - " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformLengthCompare()
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowLengthUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                LengthUnit unit1 = (LengthUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowLengthUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                LengthUnit unit2 = (LengthUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityLength(val1, unit1);
                QuantityModel q2 = new QuantityLength(val2, unit2);
                
                bool areEqual = this._service.Compare(q1, q2);
                
                if (areEqual)
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are EQUAL");
                }
                else
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are NOT EQUAL");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformLengthConvert()
        {
            try
            {
                Console.WriteLine("\nEnter value to convert:");
                string valInput = Console.ReadLine();
                double val = double.Parse(valInput);
                
                Console.WriteLine("Select from unit:");
                this.ShowLengthUnits();
                string fromUnitInput = Console.ReadLine();
                int fromUnitChoice = int.Parse(fromUnitInput);
                LengthUnit fromUnit = (LengthUnit)(fromUnitChoice - 1);
                
                Console.WriteLine("Select to unit:");
                this.ShowLengthUnits();
                string toUnitInput = Console.ReadLine();
                int toUnitChoice = int.Parse(toUnitInput);
                LengthUnit toUnit = (LengthUnit)(toUnitChoice - 1);
                
                QuantityModel from = new QuantityLength(val, fromUnit);
                QuantityModel to = new QuantityLength(0, toUnit);
                
                QuantityDTO result = this._service.Convert(from, to);
                
                Console.WriteLine("\n" + val + " " + fromUnit + " = " + result.Result + " " + toUnit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void ShowLengthUnits()
        {
            Console.WriteLine("1. Inch");
            Console.WriteLine("2. Foot");
            Console.WriteLine("3. Yard");
            Console.WriteLine("4. Mile");
            Console.WriteLine("5. Centimeter");
            Console.WriteLine("6. Meter");
            Console.WriteLine("7. Kilometer");
        }
        
        public void ShowWeightMenu()
        {
            Console.WriteLine("\n--- WEIGHT OPERATIONS ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Compare");
            Console.WriteLine("4. Convert");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                this.PerformWeightOperation("Add");
            }
            else if (choice == "2")
            {
                this.PerformWeightOperation("Subtract");
            }
            else if (choice == "3")
            {
                this.PerformWeightCompare();
            }
            else if (choice == "4")
            {
                this.PerformWeightConvert();
            }
        }
        
        private void PerformWeightOperation(string operation)
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowWeightUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                WeightUnit unit1 = (WeightUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowWeightUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                WeightUnit unit2 = (WeightUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityWeight(val1, unit1);
                QuantityModel q2 = new QuantityWeight(val2, unit2);
                
                QuantityDTO result = null;
                
                if (operation == "Add")
                {
                    result = this._service.Add(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " + " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
                else if (operation == "Subtract")
                {
                    result = this._service.Subtract(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " - " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformWeightCompare()
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowWeightUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                WeightUnit unit1 = (WeightUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowWeightUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                WeightUnit unit2 = (WeightUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityWeight(val1, unit1);
                QuantityModel q2 = new QuantityWeight(val2, unit2);
                
                bool areEqual = this._service.Compare(q1, q2);
                
                if (areEqual)
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are EQUAL");
                }
                else
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are NOT EQUAL");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformWeightConvert()
        {
            try
            {
                Console.WriteLine("\nEnter value to convert:");
                string valInput = Console.ReadLine();
                double val = double.Parse(valInput);
                
                Console.WriteLine("Select from unit:");
                this.ShowWeightUnits();
                string fromUnitInput = Console.ReadLine();
                int fromUnitChoice = int.Parse(fromUnitInput);
                WeightUnit fromUnit = (WeightUnit)(fromUnitChoice - 1);
                
                Console.WriteLine("Select to unit:");
                this.ShowWeightUnits();
                string toUnitInput = Console.ReadLine();
                int toUnitChoice = int.Parse(toUnitInput);
                WeightUnit toUnit = (WeightUnit)(toUnitChoice - 1);
                
                QuantityModel from = new QuantityWeight(val, fromUnit);
                QuantityModel to = new QuantityWeight(0, toUnit);
                
                QuantityDTO result = this._service.Convert(from, to);
                
                Console.WriteLine("\n" + val + " " + fromUnit + " = " + result.Result + " " + toUnit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void ShowWeightUnits()
        {
            Console.WriteLine("1. Milligram");
            Console.WriteLine("2. Gram");
            Console.WriteLine("3. Kilogram");
            Console.WriteLine("4. Ounce");
            Console.WriteLine("5. Pound");
            Console.WriteLine("6. Ton");
        }
        
        public void ShowVolumeMenu()
        {
            Console.WriteLine("\n--- VOLUME OPERATIONS ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Compare");
            Console.WriteLine("4. Convert");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                this.PerformVolumeOperation("Add");
            }
            else if (choice == "2")
            {
                this.PerformVolumeOperation("Subtract");
            }
            else if (choice == "3")
            {
                this.PerformVolumeCompare();
            }
            else if (choice == "4")
            {
                this.PerformVolumeConvert();
            }
        }
        
        private void PerformVolumeOperation(string operation)
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowVolumeUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                VolumeUnit unit1 = (VolumeUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowVolumeUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                VolumeUnit unit2 = (VolumeUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityVolume(val1, unit1);
                QuantityModel q2 = new QuantityVolume(val2, unit2);
                
                QuantityDTO result = null;
                
                if (operation == "Add")
                {
                    result = this._service.Add(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " + " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
                else if (operation == "Subtract")
                {
                    result = this._service.Subtract(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " - " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformVolumeCompare()
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowVolumeUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                VolumeUnit unit1 = (VolumeUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowVolumeUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                VolumeUnit unit2 = (VolumeUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityVolume(val1, unit1);
                QuantityModel q2 = new QuantityVolume(val2, unit2);
                
                bool areEqual = this._service.Compare(q1, q2);
                
                if (areEqual)
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are EQUAL");
                }
                else
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are NOT EQUAL");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformVolumeConvert()
        {
            try
            {
                Console.WriteLine("\nEnter value to convert:");
                string valInput = Console.ReadLine();
                double val = double.Parse(valInput);
                
                Console.WriteLine("Select from unit:");
                this.ShowVolumeUnits();
                string fromUnitInput = Console.ReadLine();
                int fromUnitChoice = int.Parse(fromUnitInput);
                VolumeUnit fromUnit = (VolumeUnit)(fromUnitChoice - 1);
                
                Console.WriteLine("Select to unit:");
                this.ShowVolumeUnits();
                string toUnitInput = Console.ReadLine();
                int toUnitChoice = int.Parse(toUnitInput);
                VolumeUnit toUnit = (VolumeUnit)(toUnitChoice - 1);
                
                QuantityModel from = new QuantityVolume(val, fromUnit);
                QuantityModel to = new QuantityVolume(0, toUnit);
                
                QuantityDTO result = this._service.Convert(from, to);
                
                Console.WriteLine("\n" + val + " " + fromUnit + " = " + result.Result + " " + toUnit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void ShowVolumeUnits()
        {
            Console.WriteLine("1. Milliliter");
            Console.WriteLine("2. Liter");
            Console.WriteLine("3. Gallon");
            Console.WriteLine("4. Quart");
            Console.WriteLine("5. Pint");
            Console.WriteLine("6. Cup");
            Console.WriteLine("7. Tablespoon");
            Console.WriteLine("8. Teaspoon");
        }
        
        public void ShowTemperatureMenu()
        {
            Console.WriteLine("\n--- TEMPERATURE OPERATIONS ---");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Subtract");
            Console.WriteLine("3. Compare");
            Console.WriteLine("4. Convert");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                this.PerformTemperatureOperation("Add");
            }
            else if (choice == "2")
            {
                this.PerformTemperatureOperation("Subtract");
            }
            else if (choice == "3")
            {
                this.PerformTemperatureCompare();
            }
            else if (choice == "4")
            {
                this.PerformTemperatureConvert();
            }
        }
        
        private void PerformTemperatureOperation(string operation)
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowTemperatureUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                TemperatureUnit unit1 = (TemperatureUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowTemperatureUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                TemperatureUnit unit2 = (TemperatureUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityModel();
                q1.Value = val1;
                q1.UnitType = "Temperature";
                q1.UnitName = unit1.ToString();
                
                QuantityModel q2 = new QuantityModel();
                q2.Value = val2;
                q2.UnitType = "Temperature";
                q2.UnitName = unit2.ToString();
                
                QuantityDTO result = null;
                
                if (operation == "Add")
                {
                    result = this._service.Add(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " + " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
                else if (operation == "Subtract")
                {
                    result = this._service.Subtract(q1, q2);
                    Console.WriteLine("\nResult: " + val1 + " " + unit1 + " - " + val2 + " " + unit2 + " = " + result.Result + " " + unit1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformTemperatureCompare()
        {
            try
            {
                Console.WriteLine("\nEnter first value:");
                string val1Input = Console.ReadLine();
                double val1 = double.Parse(val1Input);
                
                Console.WriteLine("Select first unit:");
                this.ShowTemperatureUnits();
                string unit1Input = Console.ReadLine();
                int unit1Choice = int.Parse(unit1Input);
                TemperatureUnit unit1 = (TemperatureUnit)(unit1Choice - 1);
                
                Console.WriteLine("\nEnter second value:");
                string val2Input = Console.ReadLine();
                double val2 = double.Parse(val2Input);
                
                Console.WriteLine("Select second unit:");
                this.ShowTemperatureUnits();
                string unit2Input = Console.ReadLine();
                int unit2Choice = int.Parse(unit2Input);
                TemperatureUnit unit2 = (TemperatureUnit)(unit2Choice - 1);
                
                QuantityModel q1 = new QuantityModel();
                q1.Value = val1;
                q1.UnitType = "Temperature";
                q1.UnitName = unit1.ToString();
                
                QuantityModel q2 = new QuantityModel();
                q2.Value = val2;
                q2.UnitType = "Temperature";
                q2.UnitName = unit2.ToString();
                
                bool areEqual = this._service.Compare(q1, q2);
                
                if (areEqual)
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are EQUAL");
                }
                else
                {
                    Console.WriteLine("\n" + val1 + " " + unit1 + " and " + val2 + " " + unit2 + " are NOT EQUAL");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void PerformTemperatureConvert()
        {
            try
            {
                Console.WriteLine("\nEnter value to convert:");
                string valInput = Console.ReadLine();
                double val = double.Parse(valInput);
                
                Console.WriteLine("Select from unit:");
                this.ShowTemperatureUnits();
                string fromUnitInput = Console.ReadLine();
                int fromUnitChoice = int.Parse(fromUnitInput);
                TemperatureUnit fromUnit = (TemperatureUnit)(fromUnitChoice - 1);
                
                Console.WriteLine("Select to unit:");
                this.ShowTemperatureUnits();
                string toUnitInput = Console.ReadLine();
                int toUnitChoice = int.Parse(toUnitInput);
                TemperatureUnit toUnit = (TemperatureUnit)(toUnitChoice - 1);
                
                QuantityModel from = new QuantityModel();
                from.Value = val;
                from.UnitType = "Temperature";
                from.UnitName = fromUnit.ToString();
                
                QuantityModel to = new QuantityModel();
                to.Value = 0;
                to.UnitType = "Temperature";
                to.UnitName = toUnit.ToString();
                
                QuantityDTO result = this._service.Convert(from, to);
                
                Console.WriteLine("\n" + val + " " + fromUnit + " = " + result.Result + " " + toUnit);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        
        private void ShowTemperatureUnits()
        {
            Console.WriteLine("1. Celsius");
            Console.WriteLine("2. Fahrenheit");
            Console.WriteLine("3. Kelvin");
        }
        
        public void ShowHistoryMenu()
        {
            Console.WriteLine("\n--- HISTORY ---");
            Console.WriteLine("1. View All Measurements");
            Console.WriteLine("2. View by Operation");
            Console.WriteLine("3. View by Unit Type");
            Console.WriteLine("4. View Statistics");
            Console.WriteLine("5. Clear All History");
            Console.WriteLine("6. Back to Main Menu");
            Console.Write("Enter your choice: ");
            
            string choice = Console.ReadLine();
            
            if (choice == "1")
            {
                this.ShowAllMeasurements();
            }
            else if (choice == "2")
            {
                this.ShowByOperation();
            }
            else if (choice == "3")
            {
                this.ShowByUnitType();
            }
            else if (choice == "4")
            {
                this.ShowStatistics();
            }
            else if (choice == "5")
            {
                this.ClearHistory();
            }
        }
        
        private void ShowAllMeasurements()
        {
            List<QuantityDTO> measurements = this._service.GetAllMeasurements();
            
            Console.WriteLine("\n=== ALL MEASUREMENTS (Total: " + measurements.Count + ") ===");
            
            if (measurements.Count == 0)
            {
                Console.WriteLine("No measurements found.");
                return;
            }
            
            foreach (QuantityDTO m in measurements)
            {
                Console.WriteLine("[" + m.OperationDate.ToString("HH:mm:ss") + "] " + 
                    m.Operation + ": " + m.Value + " " + m.UnitName + " = " + m.Result + 
                    " (Type: " + m.UnitType + ")");
            }
        }
        
        private void ShowByOperation()
        {
            Console.Write("Enter operation (Add/Subtract/Compare/Convert): ");
            string operation = Console.ReadLine();
            
            List<QuantityDTO> measurements = this._service.GetMeasurementsByOperation(operation);
            
            Console.WriteLine("\n=== " + operation.ToUpper() + " OPERATIONS (Count: " + measurements.Count + ") ===");
            
            foreach (QuantityDTO m in measurements)
            {
                Console.WriteLine("[" + m.OperationDate.ToString("HH:mm:ss") + "] " + 
                    m.Value + " " + m.UnitName + " = " + m.Result);
            }
        }
        
        private void ShowByUnitType()
        {
            Console.Write("Enter unit type (Length/Weight/Volume/Temperature): ");
            string unitType = Console.ReadLine();
            
            List<QuantityDTO> measurements = this._service.GetMeasurementsByUnitType(unitType);
            
            Console.WriteLine("\n=== " + unitType.ToUpper() + " MEASUREMENTS (Count: " + measurements.Count + ") ===");
            
            foreach (QuantityDTO m in measurements)
            {
                Console.WriteLine("[" + m.OperationDate.ToString("HH:mm:ss") + "] " + 
                    m.Operation + ": " + m.Value + " " + m.UnitName + " = " + m.Result);
            }
        }
        
        private void ShowStatistics()
        {
            int total = this._service.GetTotalCount();
            Console.WriteLine("\n=== STATISTICS ===");
            Console.WriteLine("Total Measurements: " + total);
        }
        
        private void ClearHistory()
        {
            Console.Write("Are you sure you want to delete all history? (y/n): ");
            string confirm = Console.ReadLine();
            
            if (confirm != null && confirm.ToLower() == "y")
            {
                this._service.DeleteAll();
                Console.WriteLine("All history cleared.");
            }
        }
    }
}