using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Rank
    {
        [Key]
        public int RankId { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
