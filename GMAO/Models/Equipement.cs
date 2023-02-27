using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Equipement
    {
        [Key]
        public int NumTMM { get; set; }
        
        public string CodePiece { get; set; }
        public string NumSerie { get; set; }
        public string Emplacement { get; set; }
        public DateTime DateReception { get; set; }
        public DateTime DateAchat { get; set; }
        public DateTime DateFabrication { get; set; }
        public string Aquis { get; set; }
        public int Quantite { get; set; }
        public string ConstructFournisseur { get; set; }
        public string RefAnnexe { get; set; }
        public float PrixUnitaire { get; set; }
        public int InventaireDouane { get; set; }
        public int CentreCout { get; set; }
        public string Classement { get; set; }
        public string Obsolete { get; set; }
        public string Description { get; set; }
        public string Emplacement2 { get; set; }

        public Equipement()
        {

        }

        public Equipement(int numTMM, string codePiece, string numSerie, string emplacement, DateTime dateReception, DateTime dateAchat, DateTime dateFabrication, string aquis, int quantite, string constructFournisseur, string refAnnexe, float prixUnitaire, int inventaireDouane, int centreCout, string classement, string obsolete, string description, string emplacement2)
        {
            NumTMM = numTMM;
            CodePiece = codePiece;
            NumSerie = numSerie;
            Emplacement = emplacement;
            DateReception = dateReception;
            DateAchat = dateAchat;
            DateFabrication = dateFabrication;
            Aquis = aquis;
            Quantite = quantite;
            ConstructFournisseur = constructFournisseur;
            RefAnnexe = refAnnexe;
            PrixUnitaire = prixUnitaire;
            InventaireDouane = inventaireDouane;
            CentreCout = centreCout;
            Classement = classement;
            Obsolete = obsolete;
            Description = description;
            Emplacement2 = emplacement2;
        }




    }
}
