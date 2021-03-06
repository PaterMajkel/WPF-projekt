using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Patrol
    {
        [Key]
        public int PatrolId { get; set; }
        public int PoliceCarId { get; set; }
        public PoliceCar PoliceCar { get; set; }
        public ICollection<Policeman> Policemans { get; set; }
        public string Start_Date { get; set; }
        public string Data_zakonczenia { get; set; }
        public string Start_Hour { get; set; }
        public string End_hour { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
