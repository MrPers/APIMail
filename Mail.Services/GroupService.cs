using AutoMapper;
using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GroupService(IUserRepository userRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task RegisterAsync(GroupDto group)
        {
            await _groupRepository.Add(group);
        }

        public async Task<List<GroupDto>> GetAll()
        {
            var groups = await _groupRepository.GetAll();
            foreach (var item in groups)
            {
                item.UsersId = await _userRepository.FindAllUsersOnGroup(item.Id);

            }

            return groups;
        }

        public async Task Delete(long Id)
        {
            await _groupRepository.Delete(Id);
        }

        public async Task Update(long Id, GroupDto table)
        {
            await _groupRepository.Update(Id, table);
            await _groupRepository.SaveChanges();
        }
    }
}
