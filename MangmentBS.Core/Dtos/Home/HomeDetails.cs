using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Home
{
    public class HomeDetails
    {
        public double TotalFees { get; set; }
        public double TotalResived { get; set; }
        public double TotalRest { get; set; }
        public List<HomeInstallmentDetails> PerviousInstallments { get; set; }
        public List<HomeInstallmentDetails> CurrentInstallments { get; set; }
        public List<HomeInstallmentDetails> NextInstallments { get; set; }
    }
}
