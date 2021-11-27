using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Mail.Business.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(UserDto user)
        {
            if (user==null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.GetIDAddAsync(user);
        }

        public async Task<ICollection<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users;
        }

        public async Task DeleteAsync([Range(1, long.MaxValue)] long id)
        {

            await _userRepository.DeleteAsync(id);
        }

        public async Task UpdateAsync([Range(1, long.MaxValue)] long id, UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.UpdateAsync(id, user);
        }

        public async Task SubscriptionToGroupsAsync([Range(1, long.MaxValue)] long IdGroup, ICollection<long> IdUsers) // rename
        {
            if (IdUsers.Count == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            foreach (var item in IdUsers)
            {
              

                await _userRepository.SubscriptionToGroupsAsync(IdGroup, item);
            }

            await _userRepository.SaveChangesAsync();
        }

        public async Task UnsubscriptionToGroupsAsync([Range(1, long.MaxValue)]  long IdGroup, ICollection<long> IdUsers) // rename
        {
            if (IdUsers.Count == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            foreach (var item in IdUsers)
            {

                await _userRepository.UnsubscriptionToGroupsAsync(IdGroup, item);
            }

            await _userRepository.SaveChangesAsync();
        }
    }
}
