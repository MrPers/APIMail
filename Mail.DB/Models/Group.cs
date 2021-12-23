using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mail.DB.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Group : BaseEntity<long>
    {
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string Name { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
