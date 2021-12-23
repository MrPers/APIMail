using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mail.DB.Models
{
    public class Letter : BaseEntity<long>
    {
        public DateTime DepartureСreation { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string TextSubject { get; set; }
        public string TextBody { get; set; }
        public ICollection<LetterUser> LetterUsers { get; set; } = new List<LetterUser>(); 
    }
}
