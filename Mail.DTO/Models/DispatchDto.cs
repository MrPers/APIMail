using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DTO.Models
{
    public class DispatchDto
    {
        public long Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool Status { get; set; }
        public long UserId { get; set; }
    }
}
