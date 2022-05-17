using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DTO
{
    public class PoliceStation_City_Region
    {
        public int PoliceStationId { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string CityName { get; set; }
        public string DangerStage { get; set; }
        public string Address { get; set; }


    }
}
