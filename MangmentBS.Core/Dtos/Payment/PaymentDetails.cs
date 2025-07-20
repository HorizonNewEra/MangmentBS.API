using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Payment
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int BuildingId { get; set; }
        public int FlatId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string BuildingName { get; set; }
        public string BuildingLocation { get; set; }
        public int FlatNumber { get; set; }
        public double FullPrice { get; set; }
        public double StartPrice { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentDescription { get; set; }
        public int FullInstallment { get; set; }
        public int RestInstallment { get; set; }
        public double InstallmentPrice { get; set; }
        public int CollectingDay { get; set; }
        public List<PaymentInstallmentView> Installments { get; set; }

    }
}
