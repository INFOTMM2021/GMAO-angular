using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Intervention
    {
        
        [Key]
        public float NOInter { get; set; }
        public float NumDem { get; set; }
        public string Matricule { get; set; }
        public string Intervenant { get; set; }
        public string Categorie { get; set; }
        public float TauxHoraire { get; set; }
        public DateTime DDebutEstim { get; set; }
        public DateTime HDebutEstim { get; set; }
        public DateTime DFinEstim { get; set; }
        public DateTime HFinEstim { get; set; }

        public DateTime DDebut { get; set; }
        public DateTime HDebut { get; set; }
        public DateTime DFin { get; set; }
        public DateTime HFin { get; set; }
        public float NHeure { get; set; }
        public DateTime DateSaisie { get; set; }
        public DateTime HeureSaisie { get; set; }

        public Intervention(float nOInter, float numDem, string matricule, string intervenant, string categorie, float tauxHoraire, DateTime dDebutEstim, DateTime hDebutEstim, DateTime dFinEstim, DateTime hFinEstim, DateTime dDebut, DateTime hDebut, DateTime dFin, DateTime hFin, float nheure, DateTime dateSaisie, DateTime heureSaisie)
        {
            NOInter = nOInter;
            NumDem = numDem;
            Matricule = matricule;
            Intervenant = intervenant;
            Categorie = categorie;
            TauxHoraire = tauxHoraire;
            DDebutEstim = dDebutEstim;
            HDebutEstim = hDebutEstim;
            DFinEstim = dFinEstim;
            HFinEstim = hFinEstim;
            DDebut = dDebut;
            HDebut = hDebut;
            DFin = dFin;
            HFin = hFin;
            NHeure = nheure;
            DateSaisie = dateSaisie;
            HeureSaisie = heureSaisie;
        }
        public Intervention()
        {
        }
    }
}
