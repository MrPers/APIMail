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
        public long Id { get; set; }
        public long LetterId { get; set; }
        public long UserId { get; set; }
        [Required]
        public bool Status { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}
