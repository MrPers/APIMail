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
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task RegisterAsync(GroupDto group)
        {
            await _groupRepository.Add(group);
        }

        public async Task<List<GroupDto>> GetAll()
        {
            var groups = await _groupRepository.GetAll();

            return groups;
        }

    }
}
