using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class HomeInstallmentDetails
    {
        public int Id { get; set; }
        public DateTime InstallmentDate { get; set; }
        public string InstallmentDateString { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public double InstallmentAmount { get; set; }
    }
}
