using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class MO_PREVENTIVE
    {
        public int CodePrev { get; set; }
        [Key]
        public int CodeMO { get; set; }
        public string Categorie { get; set; }
        public string Matricule { get; set; }

        public string Intervenant { get; set; }
        public float TauxHoraire { get; set; }

        public MO_PREVENTIVE()
        {
        }

        public MO_PREVENTIVE(int codePrev, int codeMO, string categorie, string matricule, string intervenant, float tauxHoraire)
        {
            CodePrev = codePrev;
            CodeMO = codeMO;
            Categorie = categorie;
            Matricule = matricule;
            Intervenant = intervenant;
            TauxHoraire = tauxHoraire;
        }
    }
}
