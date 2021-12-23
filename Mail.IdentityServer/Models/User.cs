using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mail.IdentityServer.Models
{
    public class User : IdentityUser<long>
    {
        public User(string userName) : base(userName)
        {
        }

        [Column(TypeName = "varchar(50)")]
        public override string UserName { get; set; }
    }
}
