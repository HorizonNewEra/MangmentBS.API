using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class Building : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public double LandPrice { get; set; }
        [Required]
        public double ConstractionFees { get; set; }
        public string? Description { get; set; }
        public List<Flat> Flats { get; set; } = new List<Flat>();
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public List<BuildingContractImages> BuildingContractImages { get; set; } = new List<BuildingContractImages>();
    }
}
