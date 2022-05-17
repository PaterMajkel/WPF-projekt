using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Role { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
        public int? PolicemanId { get; set; }
        public Policeman Policeman { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
