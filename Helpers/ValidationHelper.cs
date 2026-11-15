using System;

namespace RepairCenter.Helpers
{
    // صف ساكن يحتوي على دوال بسيطة للتحقق من صحة البيانات.
    public static class ValidationHelper
    {
        public static bool IsValidSerialNumber(string serialNumber)
        {
            return !string.IsNullOrWhiteSpace(serialNumber)
                && serialNumber.Length >= 4
                && serialNumber.Length <= 20;
        }

        public static bool IsValidDate(DateTime date)
        {
            return date >= new DateTime(2000, 1, 1)
                && date <= DateTime.Today;
        }

        public static bool IsValidCost(decimal cost)
        {
            return cost >= 0;
        }

        public static bool IsValidPositiveNumber(int number)
        {
            return number > 0;
        }
    }
}
