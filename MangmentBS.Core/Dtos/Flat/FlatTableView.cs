using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Flat
{
    public class FlatTableView
    {
        public string BuildingName { get; set; }
        public string ClientName { get; set; }
        public int FlatNumber { get; set; }
        public double StanderdPrice { get; set; }
        public int Floor { get; set; }
        public double Size { get; set; }
        public List<string> FlatContractImages { get; set; }
        public bool IsSold { get; set; } = false;
    }
}
