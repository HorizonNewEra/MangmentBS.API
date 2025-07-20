using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Params.Flat
{
    public class FlatSpecificationsParams : SpecificationsParams
    {
        public int? BuildingId { get; set; }
        public string? ClientId { get; set; }

    }
}
