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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task RegisterAsync(UserDto user)
        {
            await _userRepository.Add(user);
        }

        public async Task<List<UserDto>> GetAll(String groupName)
        {

            var group = _userRepository.GetAll();   ///
            var users = _userRepository.GetAll();

            return await users;
        }
    }
}
