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
        Task<List<UserDto>> GetAll(String groupName);

    }
}
