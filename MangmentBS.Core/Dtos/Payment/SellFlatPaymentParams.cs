using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Payment
{
    public class SellFlatPaymentParams
    {
        [Required(ErrorMessage = "Payment Method is required.")]
        [StringLength(100, ErrorMessage = "Payment Method cannot exceed 100 characters.")]
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Client ID is required.")]
        public string ClientId { get; set; }
        [Required(ErrorMessage = "Flat ID is required.")]
        public int FlatId { get; set; }
        [Required(ErrorMessage = "Start Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Start Price must be greater than zero.")]
        public double StartPrice { get; set; }
        [Required(ErrorMessage = "Full Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Full Price must be greater than zero.")]
        public double FullPrice { get; set; }
        [Required(ErrorMessage = "Full Months is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Full Months must be at least 1.")]
        public int FullMonths { get; set; }
        [Required(ErrorMessage = "Monthly Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Monthly Price must be greater than zero.")]
        public double MonthlyPrice { get; set; }
        [Required(ErrorMessage = "Collecting Date is required.")]
        public string CollectingDate { get; set; }
        [Required(ErrorMessage = "Every How Many Month is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Every How Many Month must be at least 1.")]
        public int EveryManyMonth { get; set; }
    }
}
