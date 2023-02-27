using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    
    public class DemandeTravail
    {

        
        [Key]
        public int NumDem { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Contact { get; set; }
        public string Service { get; set; }
        public string Departement { get; set; }
        public DateTime DateDem { get; set; }
        public DateTime HeureDem { get; set; }
        public string Nature { get; set; }
        public string CodeIntervention { get; set; }

        public string Consommable { get; set; }
        public string Description { get; set; }

        public string CodeEquipement { get; set; }

        public DateTime DateSouh { get; set; }
        public string CentreCout { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }

        public string TEffect { get; set; }
        public string NatPanne { get; set; }

        public string TypePanne { get; set; }
        public DateTime DateDiag { get; set; }
        public DateTime DateDInter { get; set; }
        public DateTime DateDiagClo { get; set; }
        public DateTime DatePreClo { get; set; }
        public DateTime DateClo { get; set; }
        public DateTime DateFinT { get; set; }
        public DateTime HeureFinT { get; set; }
        public DateTime DateValid { get; set; }
        public DateTime HeureValid { get; set; }
        public DateTime DateValidSys { get; set; }
        public DateTime HeureValidSys { get; set; }
        public string Reception { get; set; }

        public string ArretMachine { get; set; }
        public string MotifValid { get; set; }

        public int NumEquipement { get; set; }

        public string CodeEmplacement { get; set; }

        public DemandeTravail(int numdem, int userId, string userName, string contact, string service, string departement, DateTime dateDem, DateTime heureDem, string nature, string codeIntervention, string consommable, string description, string codeEquipement, DateTime dateSouh, string centreCout, string designation, string status, string tEffect, string naturePanne, string typePanne, DateTime dateDiag, DateTime dateDInter, DateTime dateDiagClo, DateTime datePreClo, DateTime dateClo, DateTime dateFinT, DateTime heureFinT, DateTime dateValid, DateTime heureValid, string reception, string arretMachine, string motifValid, int numEquipement, string codeEmplacement)
        {
            NumDem = numdem;
            Contact = contact;
            Nature = nature;
            Consommable = consommable;
            Description = description;
            CentreCout = centreCout;
            Service = service;
            CodeEquipement = codeEquipement;
            DateSouh = dateSouh;
            CodeIntervention = codeIntervention;
            Departement = departement;
            DateDem = dateDem;
            HeureDem = heureDem;
            UserId = userId;
            UserName = userName;
            Designation = designation;
            Status = status;
            TEffect = tEffect;
            NatPanne = naturePanne;
            TypePanne = typePanne;
            DateDiag = dateDiag;
            DateDInter = dateDInter;
            DateDiagClo = dateDiagClo;
            DatePreClo = datePreClo;
            DateClo = dateClo;
            DateFinT = dateFinT;
            HeureFinT = heureFinT;
            DateValid = dateValid;
            HeureValid = heureValid;
            Reception = reception;
            ArretMachine = arretMachine;
            MotifValid = motifValid;
            NumEquipement = numEquipement;
            CodeEmplacement = codeEmplacement;
        }

        public DemandeTravail()
        {
        }

        //public DemandeTravail(int numdem, string contact, DateTime dateDem, string description, string centreCout, string status, DateTime datesouh, string typepanne)
        //{
        //    NumDem = numdem;
        //    Contact = contact;
        //    DateDem = DateTime.Now;
        //    Description = description;
        //    CentreCout = centreCout;
        //    Status = status;
        //    DateSouh = datesouh;
        //    TypePanne = typepanne;
        //}
    }
}
