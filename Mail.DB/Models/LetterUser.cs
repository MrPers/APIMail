using System;

namespace Mail.DB.Models
{
    public class LetterUser : BaseEntity<long>
    {
        public long LetterId { get; set; }
        public Letter Letter { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public bool Status { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}