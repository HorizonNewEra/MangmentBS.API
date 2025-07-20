using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Helper
{
    public static class HelperFn
    {
        public static DateTime GetDateFormat(DateTime date, string format = "dd/MM/yyyy") => DateTime.Parse(date.ToString(format));
        public static int GetYear(DateTime date) => date.Year;
        public static int GetMonth(DateTime date) => date.Month;
        public static int GetDay(DateTime date) => date.Day;
        public static string GetDateString(DateTime date) => date.ToString("dd/MM/yyyy");
        public static string? GetStringFromDate(DateTime? date)
        {
            if (date != null)
                return date.Value.ToString("dd/MM/yyyy");
            return null;
        }
        public static string GetDateTimeString(DateTime date) => date.ToString("dd/MM/yyyy HH:mm:ss");
        public static string GetTimeString(DateTime date) => date.ToString("HH:mm:ss");
        public static string GetDateTimeString(DateTime date, string format) => date.ToString(format);
        public static DateTime? GetDateFromString(string date)
        {  
            try
            {
                DateTime dateTime = DateTime.Parse(date);
                return GetDateFormat(dateTime);
            }
            catch
            {
                return null;
            }
        }
        public static string? MonthNameEng(int month)
        {
            return month switch
            {
                1 => "January",
                2 => "February",
                3 => "March",
                4 => "April",
                5 => "May",
                6 => "June",
                7 => "July",
                8 => "August",
                9 => "September",
                10 => "October",
                11 => "November",
                12 => "December",
                _ => null
            };
        }
        public static string? MonthNameAr(int month)
        {
            return month switch
            {
                1 => "يناير",
                2 => "فبراير",
                3 => "مارس",
                4 => "أبريل",
                5 => "مايو",
                6 => "يونيو",
                7 => "يوليو",
                8 => "أغسطس",
                9 => "سبتمبر",
                10 => "أكتوبر",
                11 => "نوفمبر",
                12 => "ديسمبر",
                _ => null
            };
        }
    }
}
