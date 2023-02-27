using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class NaturePiece
    {
        [Key]
        public string NatPiece { get; set; }

        public NaturePiece(string natPiece)
        {
            NatPiece = natPiece;
        }
        public NaturePiece()
        {
          
        }
    }
}
