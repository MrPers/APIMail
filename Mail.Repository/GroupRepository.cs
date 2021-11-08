using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using System;

namespace Mail.Repository
{
    public class GroupRepository : BaseRepository<Group, GroupDto, long>, IGroupRepository
    {
        public GroupRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}