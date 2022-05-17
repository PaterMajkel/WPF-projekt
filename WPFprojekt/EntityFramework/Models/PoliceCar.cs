using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class PoliceCar
    {
        [Key]
        public int PoliceCarId { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int ProductionYear { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
