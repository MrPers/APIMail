using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class UserRepository : BaseRepository<User, UserDto, long>, IUserRepository
    {
        public UserRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<ICollection<long>> GetUsersIdOnGroupAsync([Range(1, long.MaxValue)] long groupId)
        {
            var usersId = await _context.Users
               .Where(p => p.Groups.Any(y => y.Id == groupId))
               .Select(p => p.Id)
               .ToListAsync();

            return usersId;
        }

        public async Task SubscriptionToGroupsAsync([Range(1, long.MaxValue)] long groupId, [Range(1, long.MaxValue)] long userId)
        {
            var user = await _context.Users
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            user.Groups.Add(
                _context.Groups
                .FirstOrDefault(p => p.Id == groupId)
            );

        }

        public async Task UnsubscriptionToGroupsAsync([Range(1, long.MaxValue)] long groupId, [Range(1, long.MaxValue)] long userId)
        {
            var user = await _context.Users
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.Id == userId);

            if (user == null)
            {
                throw new ArgumentException(nameof(user));
            }

            user.Groups.Remove(
                _context.Groups
                .FirstOrDefault(p => p.Id == groupId)
            );

        }
    }
}