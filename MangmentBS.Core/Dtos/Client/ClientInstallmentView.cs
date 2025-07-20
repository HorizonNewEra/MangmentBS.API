using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Client
{
    public class ClientInstallmentView
    {
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public bool IsPaid { get; set; } = false;
        public string PaidDateString { get; set; }
        public string PaidDateTime { get; set; }
    }
}
