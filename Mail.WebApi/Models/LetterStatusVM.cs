using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class LetterStatusVM
    {
        [Required] 
        public DateTime DepartureDate { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required] 
        public long UserId { get; set; }
    }
}
