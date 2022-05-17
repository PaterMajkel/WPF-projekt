using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Region_City
    {
        [Key]
        public int Region_CityId { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string Name { get; set; }
        public string DangerStage { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
