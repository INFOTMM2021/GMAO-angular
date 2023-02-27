using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class TypeMouvement
    {
        [Key]
        public string Type { get; set; }
        public string Description { get; set; }

        public TypeMouvement()
        {
        }

        public TypeMouvement(string type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}
