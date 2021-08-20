using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopUI.Helpers
{
    public class CurrencyHelper
    {
        // TODO - Remove the use of this method
        public string RemoveCurrencyFromString(string stringValue)
        {
            if (stringValue.StartsWith("£"))
            {
                stringValue.Remove(0, 1);
            }

            return stringValue;
        }

        public string ConvertDecimalToCurrencyString(decimal decimalValue)
        {
            return string.Format("{0:0.00}", decimalValue);
        }

        // Converts an int value to a decimal value and returns a sting formtted as 0.00
        public string ConvertIntToCurrencyString(int intValue)
        {
            decimal decimalValue = Convert.ToDecimal(intValue);
            decimal twoDecimalplaceValue = decimal.Divide(decimalValue, 100m);
            decimal currencyValue = Math.Round(twoDecimalplaceValue, 2);

            return string.Format("{0:0.00}", currencyValue);
        }

        // Checks if string Value precedes with £, removes it and converts the string value to decimal value.
        public decimal ConvertCurrencyStringToDecimal(string stringValue)
        {
            // TODO - Refactor to remove the use of the if statement
            //if (stringValue.StartsWith("£"))
            //{
            //    stringValue = stringValue.Remove(0, 1);
            //}

            decimal decimalValue = Convert.ToDecimal(stringValue);

            return decimalValue;
        }

        public int ConvertCurrencyStringToInt(string stringValue)
        {
            // TODO - Refactor to remove the use of the if statement
            if (stringValue != null && stringValue.StartsWith("£"))
            {
                stringValue = stringValue.Remove(0, 1);
            }

            decimal decimalValue = Convert.ToDecimal(stringValue);
            int intValue = Convert.ToInt32(Math.Truncate(decimalValue * 100m));

            return intValue;
        }

        public int ConvertDecimalToInt(decimal decimalValue)
        {
            int intValue = Convert.ToInt32(Math.Truncate(decimalValue * 100m));

            return intValue;
        }

        public decimal CalculateTax(decimal decimalValue)
        {
            return decimalValue - (decimalValue / 1.2m);
        }

        public decimal ConvertIntToDecimalCurrency()
        {
            // Add logic
            return 0m;
        }
    }
}
