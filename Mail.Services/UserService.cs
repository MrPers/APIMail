using AutoMapper;
using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _groupRepository = groupRepository;
        }

        public async Task RegisterAsync(UserDto user)
        {
            await _userRepository.Add(user);
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAll();

            return users;
        }

        public async Task Delete(long Id)
        {
            await _userRepository.Delete(Id);
        }

        public async Task Update(long Id, UserDto table)
        {
            await _userRepository.Update(Id, table);
            await _userRepository.SaveChanges();
        }

        public async Task AddInGroups(long IdGroup, long[] IdUsers)
        {
            for (int i = 0; i < IdUsers.Length; i++)
            {

                await _userRepository.AddInGroups(IdGroup, IdUsers[i]);
            }

            await _userRepository.SaveChanges();
        }

        public async Task DeleteWithGroups(long IdGroup, long[] IdUsers)
        {
            for (int i = 0; i < IdUsers.Length; i++)
            {

                await _userRepository.DeleteWithGroups(IdGroup, IdUsers[i]);
            }

            await _userRepository.SaveChanges();
        }
    }
}
