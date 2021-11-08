using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IGroupService groupService, IMapper mapper)
        {
            _userService = userService;
            _groupService = groupService;
            _mapper = mapper;
        }

        [HttpPost("registuser")]
        public async Task<IActionResult> RegisterUser(UserVM user)
        {
            await _userService.RegisterAsync(_mapper.Map<UserDto>(user));

            return Ok();
        }

        [HttpPost("registgroup")]
        public async Task<IActionResult> RegisterGroup(GroupVM group)
        {
            await _groupService.RegisterAsync(_mapper.Map<GroupDto>(group));

            return Ok();
        }

        [HttpGet("getuserall/{groupname}")]
        public async Task<IActionResult> GetUserAll(String groupName)
        {
            var users = await _userService.GetAll(groupName);
            IActionResult result = users == null ? NotFound() : Ok(users);

            //return result;
            return Ok();
        }

        [HttpGet("getgroupall")]
        public async Task<IActionResult> GetGroupAll()
        {
            var groups = await _groupService.GetAll();
            IActionResult result = groups == null ? NotFound() : Ok(groups);

            return result;
        }

        //[HttpGet("getuserid/{id}")]
        //public async Task<IActionResult> GetId(long id)
        //{


        //    return Ok();
        //}
    }
}
