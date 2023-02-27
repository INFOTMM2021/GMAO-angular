using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Mouvement
    {
        [Key]
        public float NumMvt { get; set; }
        public string Type { get; set; }
        public string Tier { get; set; }
        public DateTime DateMvt { get; set; }
        public string Ref { get; set; }
        public string CentreCout { get; set; }
        public string UserName { get; set; }
        public DateTime DateSaisie { get; set; }
        public DateTime HeureSaisie { get; set; }

        public Mouvement()
        {
        }

        public Mouvement(float numMvt, string type, string tier, DateTime dateMvt, string @ref, string centreCout, string userName, DateTime dateSaisie, DateTime heureSaisie)
        {
            NumMvt = numMvt;
            Type = type;
            Tier = tier;
            DateMvt = dateMvt;
            Ref = @ref;
            CentreCout = centreCout;
            UserName = userName;
            DateSaisie = dateSaisie;
            HeureSaisie = heureSaisie;
        }
    }
}
