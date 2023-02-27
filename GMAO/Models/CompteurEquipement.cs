using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class CompteurEquipement
    {
        [Key]
        public string CodeCompteur { get; set; }
        public string Description { get; set; }
        public string TypeCompteur { get; set; }

        public CompteurEquipement()
        {
        }

        public CompteurEquipement(string codeCompteur, string description, string typeCompteur)
        {
            CodeCompteur = codeCompteur;
            Description = description;
            TypeCompteur = typeCompteur;
        }
    }
}
