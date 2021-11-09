using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mail.DB.Models;
using Mail.DTO.Models;

namespace Mail.Contracts.Repo
{
    public interface IUserRepository : IBaseRepository<User, UserDto, long>
    {
        public Task<List<UserDto>> findAllUsersOnGroup(long groupId);
    }
}
