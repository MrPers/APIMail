using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Mail.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private ILogger<UserController> _logger;

        public UserController(
            IUserService userService,
            ILogger<UserController> logger,
            IMapper mapper
        )
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Initialization");
        }

        [Authorize("ALLAdministrator")]
        [HttpDelete("delete-user/{Id}")]
        public async Task<IActionResult> DeleteUser([Range(1, long.MaxValue)] long Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _userService.DeleteAsync(Id);

            return Ok(true);
        }

        [Authorize("User")]
        [HttpGet("get-users-all")]
        public async Task<IActionResult> GetUsersAll()
        {
            var users = await _userService.GetAllAsync();
            IActionResult result = users == null ? NotFound() : Ok(_mapper.Map<List<UserVM>>(users));

            return result;
        }

        [Authorize("ALLAdministrator")]
        [HttpPost("regist-user")]
        public async Task<IActionResult> RegisterUser([FromForm] UserVM user)
        {
            await _userService.RegisterAsync(_mapper.Map<UserDto>(user));

            return Ok(true);
        }

        [Authorize("ALLAdministrator")]
        [HttpPost("update-user")]
        public async Task<IActionResult> UpdateUser([FromForm] UserVM user)
        {
            await _userService.UpdateAsync(user.Id, _mapper.Map<UserDto>(user));

            return Ok(true);
        }

        [Authorize("ALLAdministrator")]
        [HttpPost("delete-user-group")]
        public async Task<IActionResult> DeleteUserGroup(GroupUserReplyUI statusUserGroup)
        {
            await _userService.UnsubscriptionToGroupsAsync(statusUserGroup.IdGroup, statusUserGroup.IdUsers);

            return Ok(true);
        }

        [Authorize("ALLAdministrator")]
        [HttpPost("add-user-group")]
        public async Task<IActionResult> AddUserGroup(GroupUserReplyUI statusUserGroup)
        {
            await _userService.SubscriptionToGroupsAsync(statusUserGroup.IdGroup, statusUserGroup.IdUsers);

            return Ok(true);
        }

    }
}
