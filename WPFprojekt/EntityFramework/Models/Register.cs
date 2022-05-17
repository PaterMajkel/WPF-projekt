using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Register
    {
        [Key]
        public int RegisterId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public byte[] Picture { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public ICollection<Felony> Felonys { get; set; }
        public ICollection<Crime> Crimes { get; set; }
    }
}
