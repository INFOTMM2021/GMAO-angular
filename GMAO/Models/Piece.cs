using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Piece
    {
        [Key]
        public string CodePiece { get; set; }
        public string Designation { get; set; }
        public string Unite { get; set; }
        public string NumSerie { get; set; }
        public float Pachat { get; set; }
        public string CodeFournisseur { get; set; }
        public float StockMin { get; set; }
        public float StockMax { get; set; }
        public float StockAlerte { get; set; }
        public float QteStock { get; set; }
        public string AnCode { get; set; }
        public string Machine { get; set; }
        public string CodeCC { get; set; }
        public float Previent { get; set; }
        public string RefFournisseur { get; set; }
        
        public string Analyse1 { get; set; }
        public string Analyse2 { get; set; }
        public string Analyse3 { get; set; }
        public float PAchatTTC { get; set; }
        public string ZStockage { get; set; }
        public string If_CC { get; set; }
        public string CentreCout { get; set; }
        public string Marque { get; set; }
        public string AnneeConstruction { get; set; }
        public DateTime DateMES { get; set; }
        public string Etalonnage { get; set; }
        public string Image { get; set; }
        public string FicheT { get; set; }
        public float QteCmd { get; set; }
        public float Test_CCout { get; set; }
        public DateTime DateAchat { get; set; }
        public DateTime DateFabrication { get; set; }
        public string Obsolete { get; set; }


        public override string ToString()
        {
            return this.Designation.ToString() +" "+ this.CodePiece.ToString();
        }

        public Piece()
        {

        }

        //public Piece(string codePiece, string designation, string unite, string numSerie, float pachat, string codeFournisseur, float stockAlerte, float qteStock, string anCode, string machine, string codeCC, string refFournisseur, string analyse1, string analyse2, string analyse3, float pAchatTTC, string zStockage, string if_CC, string centreCout, string marque, float qteCmd, DateTime dateAchat, DateTime dateFabrication, string obsolete, TimeSpan rowStamp)
        //{
        //    CodePiece = codePiece;
        //    Designation = designation;
        //    Unite = unite;
        //    NumSerie = numSerie;
        //    Pachat = pachat;
        //    CodeFournisseur = codeFournisseur;
        //    StockAlerte = stockAlerte;
        //    QteStock = qteStock;
        //    AnCode = anCode;
        //    Machine = machine;
        //    CodeCC = codeCC;
        //    RefFournisseur = refFournisseur;
        //    Analyse1 = analyse1;
        //    Analyse2 = analyse2;
        //    Analyse3 = analyse3;
        //    ZStockage = zStockage;
        //    If_CC = if_CC;
        //    CentreCout = centreCout;
        //    Marque = marque;
        //    QteCmd = qteCmd;
        //    DateAchat = dateAchat;
        //    DateFabrication = dateFabrication;
        //    Obsolete = obsolete;
        //    RowStamp = rowStamp;
            
        //}



        public Piece(string codePiece, string designation, string unite, string numSerie, float pachat, string codeFournisseur, float stockMin, float stockMax, float stockAlerte, float qteStock, string anCode, string machine, string codeCC, float previent, string refFournisseur, string analyse1, string analyse2, string analyse3, float pAchatTTC, string zStockage, string centreCout, string marque, string anneeConstruction, DateTime dateMes,  float qteCmd, DateTime dateAchat, DateTime dateFabrication)
        {
            CodePiece = codePiece;
            Designation = designation;
            AnCode = anCode;
            Unite = unite;
            Analyse1 = analyse1;
            DateAchat = dateAchat;
            DateFabrication = dateFabrication;
            Marque = marque; 
            Analyse2 = analyse2;
            Machine = machine;
            CentreCout = centreCout;
            NumSerie = numSerie;
            QteStock = qteStock;
            StockAlerte = stockAlerte;
            QteCmd = qteCmd;
            Pachat = pachat;
            CodeFournisseur = codeFournisseur;
            Analyse3 = analyse3;

            StockMin = stockMin;
            StockMax = stockMax;
            CodeCC = codeCC;
            Previent = previent;
            RefFournisseur = refFournisseur;
            PAchatTTC = pAchatTTC;
            ZStockage = zStockage;
            AnneeConstruction = anneeConstruction;
            DateMES = dateMes;

        }
    }
}
