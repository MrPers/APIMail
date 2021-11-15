using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class DispatchVM
    {
        public long Id { get; set; }
        [Required] 
        public DateTime DepartureDate { get; set; }
        public bool Status { get; set; }
        [Required] 
        public long UserId { get; set; }
    }
}
