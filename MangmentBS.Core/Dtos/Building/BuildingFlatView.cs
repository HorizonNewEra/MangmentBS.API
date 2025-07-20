using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Building
{
    public class BuildingFlatView
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public int FlatNumber { get; set; }
        public double StanderdPrice { get; set; }
        public int Floor { get; set; }
        public double Size { get; set; }
        public double SellingPrice { get; set; } = 0;
        public List<string> FlatContractImage { get; set; }
    }
}
