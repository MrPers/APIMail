using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class GroupVM
    {
        public long Id { get; set; }
        [Required] 
        [MinLength(4)]
        public string Name { get; set; }
        public long[] UsersId { get; set; }
    }
}
