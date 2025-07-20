using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Params.Installments
{
    public class InstallmentSpecificationsParams:SpecificationsParams
    {
        public int? PaymentId { get; set; }
        public bool? IsPaid { get; set; }

    }
}
