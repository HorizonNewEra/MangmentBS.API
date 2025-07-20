using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangmentBS.Core.Dtos.Building
{
    public class BuildingTableView
    {
        public int Id { get; set; }
        public List<string> LandContractImages { get; set; }
        public string Name { get; set; }
        public int FlatCount { get; set; }
        public string Location { get; set; }
    }
}
