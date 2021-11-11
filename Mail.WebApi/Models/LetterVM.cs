using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Models
{
    public class LetterVM
    {
        public string textSubject{ get; set; }
        public string textBody { get; set; }
        public UserVM[] users { get; set; }
    }
}
