using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class CentreCoutSeq
    {
        
        [Key]
        public string CodeCC { get; set; }
        public string Designation { get; set; }
        public string CodeAnt { get; set; }
        public int Seq { get; set; } 



        public CentreCoutSeq(){}

        public CentreCoutSeq(string codeCC, string designation, string codeAnt, int seq)
        {
            CodeCC = codeCC;
            Designation = designation;
            CodeAnt = codeAnt;
            Seq = seq;
        }


        public int getSeq() { return this.Seq; }
        public string getDesignation() { return this.Designation; }
        public string getCodeAnt() { return this.CodeAnt; }
        public string getCodeCC() { return this.CodeCC; }

    }
}
