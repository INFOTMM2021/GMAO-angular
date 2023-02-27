using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class DocumentEquipement
    {
        public string CodeEquipement { get; set; }
        [Key]
        public string CodeDocument { get; set; }
        
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string ExtentionFile { get; set; }



        public DocumentEquipement(string codeEquipement, string codeDocument, string description, DateTime dateCreation)
        {
            CodeEquipement = codeEquipement;
            CodeDocument = codeDocument;
            Description = description;
           
        }

        //public DocumentEquipement(string codeEquipement, string codeDocument, string description, DateTime dateCreation, string extentionFile)
        //{
        //    CodeEquipement = codeEquipement;
        //    CodeDocument = codeDocument;
        //    Description = description;
        //    DateCreation = dateCreation;
        //    ExtentionFile = extentionFile;
        //}

        public DocumentEquipement()
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
