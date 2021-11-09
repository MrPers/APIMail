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
    [Index(nameof(Name), IsUnique = true)]
    public class Group : BaseEntity<long>
    {
        [MaxLength(50)]
        public string Name { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
