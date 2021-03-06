using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class LetterVM
    {
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        [Required]
        public ICollection<long> UsersId { get; set; }
    }
}
