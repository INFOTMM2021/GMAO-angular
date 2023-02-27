using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class Module
    {
        [Key]
        public int IdModule { get; set; }
        public string ModuleName { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public int Rank { get; set; }
        public byte Active { get; set; }

        public Module()
        {
        }

        public Module(int idModule, string moduleName, string type, string code, int rank, byte active)
        {
            IdModule = idModule;
            ModuleName = moduleName;
            Type = type;
            Code = code;
            Rank = rank;
            Active = active;
        }
    }
}
