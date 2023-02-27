using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class MvtArt
    {
     

        [Key]
        public float     NumMvt { get; set; }
        public string    Type { get; set; }
        public string CodePiece { get; set; }
        public float QteMvt { get; set; }
        public float PrixRevient { get; set; }
        public float QteAvMvt { get; set; }
        public float PMPAvMvt { get; set; }
        public int tint { get; set; }

        public MvtArt(float numMvt, string type, string codePiece, float qteMvt, float prixRevient, float qteAvMvt, float pMPAvMvt, int tint)
        {
            NumMvt = numMvt;
            Type = type;
            CodePiece = codePiece;
            QteMvt = qteMvt;
            PrixRevient = prixRevient;
            QteAvMvt = qteAvMvt;
            PMPAvMvt = pMPAvMvt;
            this.tint = tint;
        }
        public MvtArt()
        {
        }
    }
}
