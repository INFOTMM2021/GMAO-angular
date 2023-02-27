using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class FournisseurPiéce : DbContext
    {
        public string  CodePiéce { get; set; }

        [Key]
        public string CodeFournisseur  { get; set; }
        public double PAchat { get; set; }
        public string RefFrs { get; set; }
        public double Previent { get; set; }
        public string Devise { get; set; }

        public FournisseurPiéce()
        {
        }

        public FournisseurPiéce(string codePiece, string codeFournisseur, double pAchat, string refFrs, double previent, string devise)
        {
            CodePiéce = codePiece;
            CodeFournisseur = codeFournisseur;
            PAchat = pAchat;
            RefFrs = refFrs;
            Previent =  previent;
            Devise = devise;
        }
    }
}
