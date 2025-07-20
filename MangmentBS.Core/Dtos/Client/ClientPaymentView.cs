using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Client
{
    public class ClientPaymentView
    {
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public double StartPrice { get; set; }
        public double FullPrice { get; set; }
        public int FullMonths { get; set; }
        public int RestMonths { get; set; }
        public double MonthlyPrice { get; set; }
        public int CollectingDay { get; set; }
    }
}
