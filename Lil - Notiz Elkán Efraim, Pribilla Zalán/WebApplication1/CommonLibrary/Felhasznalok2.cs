using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary
{
    public class Felhasznalok2
    {
        [Key]
        public int ID { get; set; }

        public string felhasznaloNev { get; set; }
       
        public string jelszo { get; set; }

        public string? email { get; set; }

        public string? FELHASZNALONOTIZID { get; set; }
  
    }
}
