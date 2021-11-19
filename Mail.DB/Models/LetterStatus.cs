﻿using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DB.Models
{
    public class LetterStatus : BaseEntity<long>
    {
        [Required] 
        public DateTime DepartureDate { get; set; }
        public bool? Status { get; set; }
        [Required] 
        public long UserId { get; set; }        
    }
}