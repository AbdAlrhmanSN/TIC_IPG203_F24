using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TIC_IPG203_F24
{
   public static class ValidationHelper
    {
        public static bool IsValidProductName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            if (name.Length < 3 || name.Length > 50)
                return false;

            return true;
        }

        public static bool IsValidPrice(decimal price)
        {
            return price >= 0 && price <= 10000;
        }

        public static bool IsValidStockQuantity(int quantity)
        {
            return quantity >= 0 && quantity <= 1000;
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidScentType(string scent)
        {
            if (string.IsNullOrWhiteSpace(scent))
                return false;

            string[] validScents = { "lavender", "vanilla", "rose", "jasmine", 
                                    "sandalwood", "oud", "citrus", "mint", "cinnamon" };

            return Array.Exists(validScents, s => s.Equals(scent, StringComparison.OrdinalIgnoreCase));
        }

        public static void DisplayValidationResult(string fieldName, bool isValid)
        {
            string status = isValid ? " Valid" : " Invalid";
            Console.WriteLine(status + ": " + fieldName);
        }
    }
}
