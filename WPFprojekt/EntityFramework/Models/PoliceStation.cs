using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class PoliceStation
    {
        [Key]
        public int PoliceStationId { get; set; }
        public string Address { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public int Region_CityId { get; set; }
        public Region_City Region_City { get; set; }

    }
}
