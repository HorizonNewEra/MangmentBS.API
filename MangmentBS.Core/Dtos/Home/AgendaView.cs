using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class AgendaView
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public List<AgendaDayView> DayView { get; set; }
    }
}
