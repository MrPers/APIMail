using Mail.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.IdentityServer.ViewModels
{
    public class UpdateUserRoleViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Role> Roles { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
        public bool Delete { get; set; }
    }
}
