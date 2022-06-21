using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    public class Policeman
    {
        [Key]
        public int PolicemanId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int RankId { get; set; }
        public Rank Rank { get; set; }
        public int PoliceStationId { get; set; }
        public PoliceStation PoliceStation { get; set; }
        public ICollection<Felony> Felonys { get; set; }
        public ICollection<Crime> Crimes { get; set; }
        public ICollection<Patrol> Patrols { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
    }
}
