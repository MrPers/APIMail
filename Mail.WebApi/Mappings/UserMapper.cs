using AutoMapper;
using Mail.DB.Models;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Mappings
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
        CreateMap<UserVM, UserDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<GroupVM, GroupDto>().ReverseMap();
        CreateMap<Group, GroupDto>().ReverseMap();
        CreateMap<DispatchVM, DispatchDto>().ReverseMap();
        CreateMap<Dispatch, DispatchDto>().ReverseMap();
        }
    }
}
