using System;
using System.Collections.Generic;

namespace Mail.DTO.Models
{
    public class LetterDto
    {
        public long Id { get; set; }
        public DateTime DepartureСreation { get; set; }
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        public ICollection<long> UsersId { get; set; }
    }
}
