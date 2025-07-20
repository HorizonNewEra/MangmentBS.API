using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class Payment : BaseEntity<int>
    {
        [Required]
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public int FlatId { get; set; }
        public Flat Flat { get; set; }
        [Required]
        public double StartPrice { get; set; }
        [Required]
        public double FullPrice { get; set; }
        [Required]
        public int FullMonths { get; set; }
        [Required]
        public int RestMonths { get; set; }
        [Required]
        public double MonthlyPrice { get; set; }
        [Required]
        public int CollectingDay { get; set; }
        public List<Installment> Installments { get; set; } = new List<Installment>();
    }
}
