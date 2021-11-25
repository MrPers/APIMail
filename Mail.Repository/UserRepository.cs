using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, long>, IUserRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<long[]> FindAllUsersOnGroup(long groupId)
        {
            if (groupId < 1)
            {
                throw new ArgumentException(nameof(groupId));
            }

            var usersId = await _context.Users
               .Where(p=>p.Groups.Any(y=>y.Id==groupId))
               .Select(p => p.Id)
               .ToArrayAsync();

            return usersId;
        }

        public async Task AddInGroups(long groupId, long userId)
        {
            if (groupId < 1)
            {
                throw new ArgumentException(nameof(groupId));
            }
            if (userId < 1)
            {
                throw new ArgumentException(nameof(userId));
            }

            var f = await _context.Users
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (f == null)
            {
                throw new ArgumentException(nameof(f));
            }

            f.Groups.Add(
                    _context.Groups
                    .FirstOrDefault(p => p.Id == groupId)
                );

        }

        public async Task DeleteWithGroups(long groupId, long userId)
        {
            if (groupId < 1)
            {
                throw new ArgumentException(nameof(groupId));
            }
            if (userId < 1)
            {
                throw new ArgumentException(nameof(userId));
            }

            var f = await _context.Users
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (f == null)
            {
                throw new ArgumentException(nameof(f));
            }

            f.Groups.Remove(
                    _context.Groups
                    .FirstOrDefault(p => p.Id == groupId)
                );

        }
    }
}