using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Parametre
    {
        [Key]
        public string Cle { get; set; }
        public int Valeur { get; set; }

        public Parametre()
        {
        }

        public Parametre(string cle, int valeur)
        {
            Cle = cle;
            Valeur = valeur;
        }
    }
}
