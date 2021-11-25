using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DB.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : BaseEntity<long>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [MaxLength(50)]
        [Required]
        public string Surname { get; set; }
        [MaxLength(50)]
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<LetterUser> LetterUsers { get; set; } = new List<LetterUser>();
        //public User()
        //{
        //    LetterUsers = new List<LetterUser>();
        //}
    }
}

