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

            await _userRepository.Add(user);
        }

        public async Task<ICollection<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();

            return users;
        }

        public async Task Delete([Range(1, long.MaxValue)] long id)
        {

            await _userRepository.Delete(id);
        }

        public async Task Update([Range(1, long.MaxValue)] long id, UserDto user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await _userRepository.Update(id, user);
        }

        public async Task AddInGroup([Range(1, long.MaxValue)] long IdGroup, ICollection<long> IdUsers) // rename
        {
            if (IdUsers.Count == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            foreach (var item in IdUsers)
            {
              

                await _userRepository.AddInGroups(IdGroup, item);
            }

            await _userRepository.SaveChanges();
        }

        public async Task DeleteFromGroup([Range(1, long.MaxValue)]  long IdGroup, ICollection<long> IdUsers) // rename
        {
            if (IdUsers.Count == 0)
            {
                throw new ArgumentException(nameof(IdUsers));
            }

            foreach (var item in IdUsers)
            {

                await _userRepository.DeleteWithGroups(IdGroup, item);
            }

            await _userRepository.SaveChanges();
        }
    }
}
