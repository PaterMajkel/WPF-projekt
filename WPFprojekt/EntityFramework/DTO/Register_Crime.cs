using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.DTO
{
    public class Register_Crime
    {
        public int ID_register { get; set; }
        public int ID_crime { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public int Age { get; set; }
        //photo todo
        public string FelonyName { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; } 

    }
}
