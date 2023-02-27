using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class GroupeMenu
    {
        [Key]
        public string Id { get; set; }
        public string GroupeId { get; set; }
        public string MenuId { get; set; }
        public int authorisation { get; set; }

        public GroupeMenu()
        {

        }

        public GroupeMenu(string id, string groupeId, string menuId, int authorisation)
        {
            Id = id;
            GroupeId = groupeId;
            MenuId = menuId;
            this.authorisation = authorisation;
        }
    }
}
