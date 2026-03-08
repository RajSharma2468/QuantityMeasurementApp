using System;

namespace QuantityMeasurementApp.Utils.Validators
{
    public static class InputValidator
    {
        public static string ReadString()
        {
            string input = Console.ReadLine();

            if (input == null)
                return "";

            return input;
        }

        public static int ReadInt()
        {
            return Convert.ToInt32(ReadString());
        }

        public static double ReadDouble()
        {
            return Convert.ToDouble(ReadString());
        }
    }
}