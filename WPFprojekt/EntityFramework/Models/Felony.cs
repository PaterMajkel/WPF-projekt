using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Felony
    {
        [Key]
        public int FelonyId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public ICollection<Policeman> Policemans {get; set;}
        public ICollection<Register> Registers { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
