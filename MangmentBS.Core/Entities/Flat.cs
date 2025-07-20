using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class Flat : BaseEntity<int>
    {
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public string? ClientId { get; set; }
        public Client Client { get; set; }
        [Required]
        public int FlatNumber { get; set; }
        [Required]
        public double StanderdPrice { get; set; }
        [Required]
        public int Floor { get; set; }
        [Required]
        public double Size { get; set; }
        public double? SellingPrice { get; set; }
        public List<FlatContractImages> FlatContractImages { get; set; } = new List<FlatContractImages>();
        public Payment Payment { get; set; }
        public int? PaymentId { get; set; }
        public bool IsSold { get; set; }=false;

    }
}
