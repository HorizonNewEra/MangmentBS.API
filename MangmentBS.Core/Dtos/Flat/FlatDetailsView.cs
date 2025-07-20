using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Flat
{
    public class FlatDetailsView
    {
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public string? ClientId { get; set; } = null;
        public string ClientName { get; set; }
        public string? ClientPhone { get; set; } = null;
        public int FlatNumber { get; set; }
        public double StanderdPrice { get; set; }
        public int Floor { get; set; }
        public double Size { get; set; }
        public double? SellingPrice { get; set; } = null;
        public int? PaymentId { get; set; } = null;
        public string? PaymentMethod { get; set; } = null;
        public string? Description { get; set; } = null;
        public double? StartPrice { get; set; } = null;
        public double? FullPrice { get; set; } = null;
        public int? FullMonths { get; set; } = null;
        public int? RestMonths { get; set; } = null;
        public double? MonthlyPrice { get; set; } = null;
        public int? CollectingDay { get; set; } = null;
        public bool IsSold { get; set; } = false;
        public List<string> FlatContractImages { get; set; }
        public List<FlatInstallmentView> Installments { get; set; }
    }
}
