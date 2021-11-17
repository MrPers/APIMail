using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class StatusUserGroupVM
    {
        public long IdGroup { get; set; }
        public long[] IdUsers { get; set; }
    }
}
