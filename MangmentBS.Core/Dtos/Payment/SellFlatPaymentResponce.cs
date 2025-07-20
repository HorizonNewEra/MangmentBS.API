using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Payment
{
    public class SellFlatPaymentResponce
    {
        public int? PaymentId { get; set; }=null;
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
