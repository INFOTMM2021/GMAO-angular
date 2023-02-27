using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class CentreCout
    {
        public double Numero { get; set; }
        [Key]
        public string CodeCC { get; set; }
        public string Designation { get; set; }
        public string CodeAnt { get; set; }

        public CentreCout() { }

        public CentreCout(int numero , string Codecc, string des, string codeant)
        {
            this.Numero = numero;
            this.CodeCC = Codecc;
            this.CodeAnt = codeant;
            this.Designation = des;

        }

        public override string ToString()
        {
            return Numero + " " +  CodeCC + " " + Designation + " " + CodeAnt;
        }




        private string presentCC(CentreCout p, Hashtable liens, Hashtable lst, string jd)
        {

            string r = "{text : " + jd +(p.Designation) + ",id2 : '" + p.CodeCC + "', leaf:";
            if (liens[p.CodeCC] != null)
            {
                r += "false, children: [";
                foreach (string cc in (List<string>)liens[p.CodeCC])
                {
                    r += presentCC((CentreCout)lst[cc], liens, lst, jd) + ",";
                }
                r += "]";
            }
            else r += "true";
            r += "}";
            return r;
        }
    }
}
