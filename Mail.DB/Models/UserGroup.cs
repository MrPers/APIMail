using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DB.Models
{
    public class UserGroup
    {
        public long GroupId { get; set; }
        public Group Group { get; set; } 
        public long UserId { get; set; }
        public User User { get; set; }
    }
}
