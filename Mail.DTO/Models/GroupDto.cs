using Mail.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.DTO.Models
{
    public class GroupDto : IBaseEntity<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long[] UsersId { get; set; }
    }
}
