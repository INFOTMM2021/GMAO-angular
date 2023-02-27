using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class DemandePiece
    {
        [Key]
        public Decimal NumOrdre { get; set; }
        public string CodePiece { get; set; }
        public int NumDem { get; set; }
        public string  Designation { get; set; }
        public decimal QteDem { get; set; }
        public decimal QteLiv { get; set; }
        public decimal NumBon { get; set; }
        public string   Type { get; set; }
        public DateTime DateSaisie { get; set; }
        public DateTime HeureSaisie { get; set; }

        public DemandePiece() { }

        public DemandePiece(decimal numOrdre, string codePiece, int numDem, string designation, decimal qteDem, decimal qteLiv, decimal numBon, string type, DateTime dateSaisie, DateTime heureSaisie)
        {
            NumOrdre = numOrdre;
            CodePiece = codePiece;
            NumDem = numDem;
            Designation = designation;
            QteDem = qteDem;
            QteLiv = qteLiv;
            NumBon = numBon;
            Type = type;
            DateSaisie = dateSaisie;
            HeureSaisie = heureSaisie;
        }
    }
}
