using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Employe
    {

        [Key]
        public int Matricule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Categorie { get; set; }
        public string Specialite { get; set; }
        public string Actif { get; set; }


        public Employe()
        {

        }

        public Employe(int matricule, string nom, string prenom, string categorie, string specialite, string actif)
        {
            Matricule = matricule;
            Nom = nom;
            Prenom = prenom;
            Categorie = categorie;
            this.Specialite = specialite;
            Actif = actif;
        }

    }
}
