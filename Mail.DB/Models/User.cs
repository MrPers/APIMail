using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mail.DB.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity<long>
    {

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Surname { get; set; }

        [Column(TypeName = "varchar(50)")]
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<LetterUser> LetterUsers { get; set; } = new List<LetterUser>();
    }
}
