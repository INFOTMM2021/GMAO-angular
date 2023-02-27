using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Groupe
    {
        [Key]
        public string GroupeId { get; set; }

        public Groupe(string groupeId)
        {
            GroupeId = groupeId;
        }
    }
}
