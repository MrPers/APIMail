using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using System;
using System.Collections.Generic;
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

            await _userRepository.Add(user);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();

            return users;
        }

        public async Task Delete(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            await _userRepository.Delete(id);
        }

        public async Task Update(long id, UserDto user)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.Update(id, user);
        }

        public async Task AddInGroups(long IdGroup, long[] IdUsers)
        {
            if (IdGroup < 1)
            {
                throw new ArgumentException(nameof(IdGroup));
            }

            if (IdUsers.Length == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            for (int i = 0; i < IdUsers.Length; i++)
            {
                if (IdUsers[i] < 1)
                {
                    throw new ArgumentException(nameof(IdUsers));
                }

                await _userRepository.AddInGroups(IdGroup, IdUsers[i]);
            }

            await _userRepository.SaveChanges();
        }

        public async Task DeleteWithGroups(long IdGroup, long[] IdUsers)
        {
            if (IdGroup < 1)
            {
                throw new ArgumentException(nameof(IdGroup));
            }

            if (IdUsers.Length == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            for (int i = 0; i < IdUsers.Length; i++)
            {
                if (IdUsers[i] < 1)
                {
                    throw new ArgumentException(nameof(IdUsers));
                }

                await _userRepository.DeleteWithGroups(IdGroup, IdUsers[i]);
            }

            await _userRepository.SaveChanges();
        }
    }
}
