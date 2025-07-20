using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Payment
{
    public class PaymentTableView
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string BuildingName { get; set; }
        public string FlatNumber { get; set; }
        public double FullPrice { get; set; }
        public int RestMonth { get; set; }
        public double MonthlyPrice { get; set; }
        public int CollectingDay { get; set; }
    }
}
