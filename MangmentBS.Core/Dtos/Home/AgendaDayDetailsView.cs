using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class AgendaDayDetailsView
    {
        public int InstallmentId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public double Amount { get; set; }
        public string DueDateString { get; set; }
        public string? PaiedDateString { get; set; }
        public bool IsPaid { get; set; }
    }
}
