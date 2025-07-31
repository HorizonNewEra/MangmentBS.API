using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Client
{
    public class ClientFlatView
    {
        public int FlatId { get; set; }
        public string BuildingName { get; set; }
        public int FlatNumber { get; set; }
        public int Floor { get; set; }
        public double Size { get; set; }
        public double SellingPrice { get; set; }
        public List<string> FlatContractImages { get; set; }
    }
}
