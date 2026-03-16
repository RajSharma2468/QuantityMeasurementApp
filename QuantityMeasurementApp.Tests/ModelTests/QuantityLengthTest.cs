using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementModelLayer.Models;
using QuantityMeasurementModelLayer.Enums;
using QuantityMeasurementModelLayer.Exceptions;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthTest
    {
        [TestMethod]
        public void TestLengthConversion_1FeetToInches()
        {
            QuantityLength feet = new QuantityLength(1, LengthUnit.Foot);
            QuantityLength inches = new QuantityLength(0, LengthUnit.Inch);
            
            double result = feet.ConvertTo(inches);
            
            Assert.AreEqual(12, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthConversion_1InchToFeet()
        {
            QuantityLength inches = new QuantityLength(1, LengthUnit.Inch);
            QuantityLength feet = new QuantityLength(0, LengthUnit.Foot);
            
            double result = inches.ConvertTo(feet);
            
            Assert.AreEqual(0.0833, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthConversion_1YardToFeet()
        {
            QuantityLength yard = new QuantityLength(1, LengthUnit.Yard);
            QuantityLength feet = new QuantityLength(0, LengthUnit.Foot);
            
            double result = yard.ConvertTo(feet);
            
            Assert.AreEqual(3, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthConversion_1MileToYards()
        {
            QuantityLength mile = new QuantityLength(1, LengthUnit.Mile);
            QuantityLength yard = new QuantityLength(0, LengthUnit.Yard);
            
            double result = mile.ConvertTo(yard);
            
            Assert.AreEqual(1760, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthConversion_1MeterToCentimeters()
        {
            QuantityLength meter = new QuantityLength(1, LengthUnit.Meter);
            QuantityLength cm = new QuantityLength(0, LengthUnit.Centimeter);
            
            double result = meter.ConvertTo(cm);
            
            Assert.AreEqual(100, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthConversion_1KilometerToMeters()
        {
            QuantityLength km = new QuantityLength(1, LengthUnit.Kilometer);
            QuantityLength meter = new QuantityLength(0, LengthUnit.Meter);
            
            double result = km.ConvertTo(meter);
            
            Assert.AreEqual(1000, result, 0.001);
        }
        
        [TestMethod]
        public void TestLengthAdd_5FeetPlus2Feet()
        {
            QuantityLength q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityLength q2 = new QuantityLength(2, LengthUnit.Foot);
            
            QuantityLength q3 = new QuantityLength(0, LengthUnit.Foot);
            
            double q2Converted = q2.ConvertTo(q1);
            double result = q1.Value + q2Converted;
            
            Assert.AreEqual(7, result);
        }
        
        [TestMethod]
        public void TestLengthAdd_5FeetPlus12Inches()
        {
            QuantityLength feet = new QuantityLength(5, LengthUnit.Foot);
            QuantityLength inches = new QuantityLength(12, LengthUnit.Inch);
            
            double inchesInFeet = inches.ConvertTo(feet);
            double result = feet.Value + inchesInFeet;
            
            Assert.AreEqual(6, result);
        }
        
        [TestMethod]
        public void TestLengthSubtract_5FeetMinus2Feet()
        {
            QuantityLength q1 = new QuantityLength(5, LengthUnit.Foot);
            QuantityLength q2 = new QuantityLength(2, LengthUnit.Foot);
            
            double q2Converted = q2.ConvertTo(q1);
            double result = q1.Value - q2Converted;
            
            Assert.AreEqual(3, result);
        }
        
        [TestMethod]
        public void TestLengthSubtract_5FeetMinus12Inches()
        {
            QuantityLength feet = new QuantityLength(5, LengthUnit.Foot);
            QuantityLength inches = new QuantityLength(12, LengthUnit.Inch);
            
            double inchesInFeet = inches.ConvertTo(feet);
            double result = feet.Value - inchesInFeet;
            
            Assert.AreEqual(4, result);
        }
        
        [TestMethod]
        public void TestLengthCompare_1FeetEquals12Inches()
        {
            QuantityLength feet = new QuantityLength(1, LengthUnit.Foot);
            QuantityLength inches = new QuantityLength(12, LengthUnit.Inch);
            
            double feetInMeters = LengthUnit.Foot.ToMeters(feet.Value);
            double inchesInMeters = LengthUnit.Inch.ToMeters(inches.Value);
            
            double diff = feetInMeters - inchesInMeters;
            if (diff < 0) diff = -diff;
            
            bool areEqual = diff < 0.000001;
            
            Assert.IsTrue(areEqual);
        }
        
        [TestMethod]
        public void TestLengthCompare_1FeetNotEquals13Inches()
        {
            QuantityLength feet = new QuantityLength(1, LengthUnit.Foot);
            QuantityLength inches = new QuantityLength(13, LengthUnit.Inch);
            
            double feetInMeters = LengthUnit.Foot.ToMeters(feet.Value);
            double inchesInMeters = LengthUnit.Inch.ToMeters(inches.Value);
            
            double diff = feetInMeters - inchesInMeters;
            if (diff < 0) diff = -diff;
            
            bool areEqual = diff < 0.000001;
            
            Assert.IsFalse(areEqual);
        }
        
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestLengthAddWithDifferentTypes_ThrowsException()
        {
            QuantityLength length = new QuantityLength(5, LengthUnit.Foot);
            QuantityWeight weight = new QuantityWeight(2, WeightUnit.Kilogram);
            
            length.ConvertTo(weight);
        }
    }
}