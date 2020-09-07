using System;

namespace Common.Extensions
{
    public static class DateTimeExtension
    {
        public static int CalculateAge(this System.DateTime dt)
        {
            DateTime now = System.DateTime.Today;
            int age = now.Year - dt.Year;

            if (now < dt.AddYears(age)) age--;

            return age;
        }

        public static int CalculateAge(this System.DateTime? dt)
        {
            return dt != null ? CalculateAge(Convert.ToDateTime(dt)) : -1;
        }
    }
}
