using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface IGroupService
    {
        Task RegisterAsync(GroupDto group);
        Task<List<GroupDto>> GetAll();

    }
}
