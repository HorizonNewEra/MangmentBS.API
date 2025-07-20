using MangmentBS.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Building
{
    public class BuildingDetailsView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Size { get; set; }
        public double LandPrice { get; set; }
        public int FlatCount { get; set; }
        public double ConstractionFees { get; set; }
        public string? Description { get; set; }
        public List<BuildingFlatView> Flats { get; set; }
        public List<string> BuildingContractImages { get; set; }
    }
}
