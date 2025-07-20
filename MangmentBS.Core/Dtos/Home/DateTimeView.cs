using MangmentBS.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class DateTimeView
    {
        public string Date { get; set; } = HelperFn.GetDateString(DateTime.Now);
        public string Time { get; set; } = HelperFn.GetTimeString(DateTime.Now);
        public string Datetime { get; set; } = HelperFn.GetDateTimeString(DateTime.Now);
        public DateTime DateTimeFormat { get; set; } = DateTime.Now;
    }
}
