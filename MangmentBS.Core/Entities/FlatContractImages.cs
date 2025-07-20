using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Entities
{
    public class FlatContractImages : BaseEntity<int>
    {
        public int? FlatId { get; set; }
        public Flat Flat { get; set; }
        public int? BuildingId { get; set; }
        public Building Building { get; set; }
        public string ImageLink { get; set; }
    }
}
