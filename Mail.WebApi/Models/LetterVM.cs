using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class LetterVM
    {
        public string TextSubject{ get; set; }
        public string TextBody { get; set; }
        public UserVM[] Users { get; set; }
    }
}
