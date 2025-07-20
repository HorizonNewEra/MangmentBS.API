using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class Installment : BaseEntity<int>
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }=false;
        public DateTime? PaidDate { get; set; }
    }
}
