using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class GroupUserReplyUI
    {
        [Required]
        public long IdGroup { get; set; }
        [Required] 
        public long[] IdUsers { get; set; }
    }
}
