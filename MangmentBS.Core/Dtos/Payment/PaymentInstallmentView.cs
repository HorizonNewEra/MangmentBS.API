using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Payment
{
    public class PaymentInstallmentView
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string DueDateString { get; set; }
        public bool IsPaid { get; set; }
        public string? PaidDateString { get; set; }
    }
}
