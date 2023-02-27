using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Unite
    {
        [Key]
        public string CodeUnite { get; set; }
        public string Description { get; set; }

        public Unite()
        {

        }

        public Unite(string codeUnite, string description)
        {
            CodeUnite = codeUnite;
            Description = description;
        }
    }

}
