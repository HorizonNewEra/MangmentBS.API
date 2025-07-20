using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Flat
{
    public class FlatInstallmentView
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString { get; set; }
        public bool IsPaid { get; set; } = false;
        public DateTime? PaidDate { get; set; }
        public string? PaidDateString { get; set; }
    }
}
