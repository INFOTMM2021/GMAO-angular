using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GMAO.Models;

namespace GMAO.Data
{
    public class GMAOContext : DbContext
    {
        public GMAOContext (DbContextOptions<GMAOContext> options): base(options)
        {

        }
        public DbSet<GMAO.Models.TypeMouvement> TypeMouvement { get; set; }

        public DbSet<GMAO.Models.CentreCoutSeq> CentreCoutSeq { get; set; }
        public DbSet<GMAO.Models.FournisseurPiéce> FournisseurPiéce { get; set; }

        public DbSet<GMAO.Models.Piece> Piece { get; set; }

        public DbSet<GMAO.Models.CentreCout> CentreCout { get; set; }

        public DbSet<GMAO.Models.CompteurEquipement> CompteurEquipement { get; set; }

        public DbSet<GMAO.Models.DemandePiece> DemandePiece { get; set; }

        public DbSet<GMAO.Models.DemandeTravail> DemandeTravail { get; set; }


        public DbSet<GMAO.Models.DemPreventive> DemPreventive { get; set; }

        public DbSet<GMAO.Models.Equipement> Equipement { get; set; }

        public DbSet<GMAO.Models.EquipementCompteur> EquipementCompteur { get; set; }

        public DbSet<GMAO.Models.Groupe> Groupe { get; set; }

        public DbSet<GMAO.Models.GroupeMenu> GroupeMenu { get; set; }

        public DbSet<GMAO.Models.Intervention> Intervention { get; set; }

        public DbSet<GMAO.Models.MO_PREVENTIVE> MO_PREVENTIVE { get; set; }

        public DbSet<GMAO.Models.Module> Module { get; set; }

        public DbSet<GMAO.Models.Mouvement> Mouvement { get; set; }

        public DbSet<GMAO.Models.MouvementDetail> MouvementDetail { get; set; }

        public DbSet<GMAO.Models.MvtArt> MvtArt { get; set; }

        public DbSet<GMAO.Models.NaturePiece> NaturePiece { get; set; }

        public DbSet<GMAO.Models.Parametre> Parametre { get; set; }

        public DbSet<GMAO.Models.Preventive> Preventive { get; set; }

        public DbSet<GMAO.Models.PreventiveEquipement> PreventiveEquipement { get; set; }

        public DbSet<GMAO.Models.User> User { get; set; }

        public DbSet<GMAO.Models.Employe> Employe { get; set; }
    }
}
