using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mail.DB.Models;
using Mail.DTO.Models;

namespace Mail.Contracts.Repo
{
    public interface IGroupRepository : IBaseRepository<Group, GroupDto, long>
    {
        //public Task<long> GetId(string groupName);
    }
}
