using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Params.Payment
{
    public class PaymentSpecificationsParams:SpecificationsParams
    {
        public string? ClientId { get; set; }
        public int? BuildingId { get; set; }
        public int? FlatId { get; set; }
        public int? CollectingDay { get; set; }

    }
}
