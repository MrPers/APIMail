using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IDispatchService _dispatchService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IGroupService groupService, IMapper mapper, IDispatchService dispatchService)
        {
            _userService = userService;
            _groupService = groupService;
            _dispatchService = dispatchService;
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

        
        
        [HttpGet("getuserongroup/{groupId}")]
        public async Task<IActionResult> GetUserGroup(long groupId)
        {
            var users = await _userService.GetAll(groupId);
            IActionResult result = users == null ? NotFound() : Ok(_mapper.Map<List<UserVM>>(users));

            return result;
        }

        [HttpGet("getuserall")]
        public async Task<IActionResult> GetUserAll()
        {
            var users = await _userService.GetAll();
            IActionResult result = users == null ? NotFound() : Ok(_mapper.Map<List<UserVM>>(users));

            return result;
        }

        
        
        [HttpGet("getgroupall")]
        public async Task<IActionResult> GetGroupAll()
        {
            var groups = await _groupService.GetAll();
            IActionResult result = groups == null ? NotFound() : Ok(_mapper.Map<List<GroupVM>>(groups));

            return result;
        }

        [HttpGet("deletegroup/{Id}")]
        public async Task<IActionResult> DeleteGroup(long Id)
        {
            await _groupService.Delete(Id);

            return Ok();
        }

        [HttpGet("deleteuser/{Id}")]
        public async Task<IActionResult> DeleteUser(long Id)
        {
            await _userService.Delete(Id);

            return Ok();
        }

        [HttpPost("updategroup")]
        public async Task<IActionResult> UpdateGroup([FromForm] GroupVM group)
        {
            await _groupService.Update(group.Id, _mapper.Map<GroupDto>(group));

            return Ok();
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser([FromForm]UserVM user)
        {
            await _userService.Update(user.Id, _mapper.Map<UserDto>(user));

            return Ok();
        }

        [HttpPost("sendletter")]
        public async Task<IActionResult> sendLetter(LetterVM letter)
        {
            await _dispatchService.Add(letter.textLetter, _mapper.Map<UserDto[]>(letter.users));

            return Ok();
        }

        //[HttpPost("sendletter1")]
        //public async Task<IActionResult> sendLetter()
        //{
        //    Response.StatusCode = 200;
        //    Response.ContentType = "text/event-stream";
        //    Response.ContentLength = 10;

        //    var sw = new StreamWriter(Response.Body);

        //    for (var i = 0; i < 10; i++)
        //    {
        //        await Task.Delay(1000);
        //        await sw.WriteAsync("1");
        //        await sw.FlushAsync();
        //    }
        //    return Ok();
        //}
    }
}
