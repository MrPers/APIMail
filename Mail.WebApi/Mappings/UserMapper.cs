using AutoMapper;
using Mail.DB.Models;
using Mail.DTO.Models;
using Mail.WebApi.Models;

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
            CreateMap<LetterVM, LetterDto>().ReverseMap();
            CreateMap<Letter, LetterDto>().ReverseMap();
            CreateMap<LetterUser, LetterStatusDto>().ReverseMap();
            CreateMap<LetterStatusVM, LetterStatusDto>().ReverseMap();

        }
    }
}
