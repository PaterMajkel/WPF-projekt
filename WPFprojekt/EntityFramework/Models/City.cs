using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
