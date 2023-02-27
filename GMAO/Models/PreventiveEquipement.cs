using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class PreventiveEquipement
    {
        public PreventiveEquipement()
        {
        }

        [Key]
        public int CodePrev { get; set; }
        public int CodeEquipement { get; set; }
        public string CompteurEquipement { get; set; }
        public float Condition { get; set; }
        public DateTime DateDernierExecution { get; set; }
        public float ValeurDernierExecution { get; set; }
        public DateTime DateProchaineexecution { get; set; }
        public float ValeurProchianeExecution { get; set; }
        public DateTime DateCreation { get; set; }
        public int Statut { get; set; }
        public DateTime DateActivite { get; set; }
        public DateTime DateInactivite { get; set; }

        public PreventiveEquipement(int codePrev, int codeEquipement, string compteurEquipement, float condition, DateTime dateDernierExecution, float valeurDernierExecution, DateTime dateProchaineexecution, float valeurProchianeExecution, DateTime dateCreation, int statut, DateTime dateActivite, DateTime dateInactivite)
        {
            CodePrev = codePrev;
            CodeEquipement = codeEquipement;
            CompteurEquipement = compteurEquipement;
            Condition = condition;
            DateDernierExecution = dateDernierExecution;
            ValeurDernierExecution = valeurDernierExecution;
            DateProchaineexecution = dateProchaineexecution;
            ValeurProchianeExecution = valeurProchianeExecution;
            DateCreation = dateCreation;
            Statut = statut;
            DateActivite = dateActivite;
            DateInactivite = dateInactivite;
        }
    }
}
