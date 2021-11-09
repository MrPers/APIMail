using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class DispatchVM
    {
        public long Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool Status { get; set; }
        public long UserId { get; set; }
    }
}
