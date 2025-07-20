using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class AgendaDayView
    {
        public string DateString { get; set; }
        public List<AgendaDayDetailsView> DayDetails { get; set; }
        public int Count { get; set; }
    }
}
