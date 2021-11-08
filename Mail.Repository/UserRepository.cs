using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, long>, IUserRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<UserDto>> findAllUsersOnGroup(String groupName)
        {
            //var users = _context.Users.Include(p => p.Groups).Where(p => p.Name == groupName);
            
            //return _mapper.Map<List<UserDto>>(users);
            return null;
        }

    }
}