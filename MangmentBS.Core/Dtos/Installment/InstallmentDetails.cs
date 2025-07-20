using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Installment
{
    public class InstallmentDetails
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public string ClientId { get; set; }
        public int BuildingId { get; set; }
        public int FlatId { get; set; }
        public double Amount { get; set; }
        public string DueDateString { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? PaidDateString { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientAddress { get; set; }
        public string BuildingName { get; set; }
        public string BuildingLocation { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }
        public double FlatSize { get; set; }
        public double SellingPrice { get; set; }

    }
}
