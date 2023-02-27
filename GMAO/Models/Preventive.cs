using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Preventive
    {

        [Key]
        public int CodePrev { get; set; }
        public string Description { get; set; }
        public string TypeIntervention { get; set; }



        public Preventive( string description, string typeIntervention)
        {
            //CodePrev = codePrev;
            Description = description;
            TypeIntervention = typeIntervention;
        }

        public Preventive()
        {
        }
    }



}
