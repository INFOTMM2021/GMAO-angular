using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class EquipementCompteur
    {
        
        public string CodeEquipement { get; set; }
        public string NumSerie { get; set; }
        public int CodeTMM { get; set; }
        [Key]
        public string   CodeCompteur { get; set; }
        public DateTime Dernieremodif { get; set; }
        public float Valeur { get; set; }

        public EquipementCompteur()
        {
        }

        public EquipementCompteur(string codeEquipement, string numSerie, int codeTMM, string codeCompteur, DateTime dernierModif, float valeur)
        {
            CodeEquipement = codeEquipement;
            NumSerie = numSerie;
            CodeTMM = codeTMM;
            CodeCompteur = codeCompteur;
            Dernieremodif = dernierModif;
            Valeur = valeur;
        }
    }
}
