using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class MouvementDetail
    {
        
        [Key]
        public float NumMvt { get; set; }
        public string Type { get; set; }
        public string CodePiece { get; set; }
        public string NumSerie { get; set; }
        public float QteDem { get; set; }
        public float QteMvt { get; set; }
        public float PAchat { get; set; }
        public float PRevient { get; set; }
        public string Devise { get; set; }
        public string RefFournisseur { get; set; }
        public int tint { get; set; }


        public MouvementDetail()
        {
        }
        public MouvementDetail(float numMvt, string type, string codePiece, string numSerie, float qteDem,float qteMvt, float pAchat, float previent, string devise, string refFournisseur, int tint)
        {
            NumMvt = numMvt;
            Type = type;
            CodePiece = codePiece;
            NumSerie = numSerie;
            QteDem = qteDem;
            QteMvt = qteMvt;
            PAchat = pAchat;
            PRevient = previent;
            Devise = devise;
            RefFournisseur = refFournisseur;
            this.tint = tint;
        }
    }
}
