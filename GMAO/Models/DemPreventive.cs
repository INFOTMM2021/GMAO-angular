using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class DemPreventive
    {
        [Key]
        public float NumDem { get; set; }
        public string CCRacine { get; set; }
        public string CodeEquipement { get; set; }
        public string Designation { get; set; }

        public DemPreventive()
        {
        }

        public DemPreventive(float numDem, string cCRacine, string codeEquipement, string designation)
        {
            NumDem = numDem;
            CCRacine = cCRacine;
            CodeEquipement = codeEquipement;
            Designation = designation;
        }
    }
}
