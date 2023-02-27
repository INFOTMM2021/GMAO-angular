using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GMAO.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserGroup { get; set; }
        public string UserDep { get; set; }
        public string UserService { get; set; }

        public User(int userId, string userName, string userPwd, string userGroup, string userDep, string userService)
        {
            UserId = userId;
            UserName = userName;
            UserPwd = userPwd;
            UserGroup = userGroup;
            UserDep = userDep;
            UserService = userService;
        }

        public User()
        {
        }
    }
}
