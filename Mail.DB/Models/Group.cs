using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mail.DB.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Group : BaseEntity<long>
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
