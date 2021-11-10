using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterAsync(UserDto user);
        Task<List<UserDto>> GetAll();
        Task Delete(long Id);
        Task<List<UserDto>> GetAll(long groupId);
        Task Update(long Id, UserDto table);

    }
}
