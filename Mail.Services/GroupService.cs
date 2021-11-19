﻿using AutoMapper;
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

        public GroupService(IUserRepository userRepository, IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task RegisterAsync(GroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

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

        public async Task Delete(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            await _groupRepository.Delete(id);
        }

        public async Task Update(long id, GroupDto group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            await _groupRepository.Update(id, group);
        }
    }
}
