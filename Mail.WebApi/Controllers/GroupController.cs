using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Mail.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private ILogger<GroupController> _logger;

        public GroupController(
            IGroupService groupService,
            ILogger<GroupController> logger,
            IMapper mapper
        )
        {
            _groupService = groupService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Initialization");
        }

        [Authorize]
        [HttpGet("get-groups-all")]
        public async Task<IActionResult> GetGroupAll()
        {
            var groups = await _groupService.GetAllAsync();

            IActionResult result = groups == null ? NotFound() : Ok(_mapper.Map<List<GroupVM>>(groups));

            return result;
        }

        [Authorize("GroupAdministrator")]
        [HttpPost("regist-group")]
        public async Task<IActionResult> RegisterGroup([FromForm] GroupVM group)
        {
            await _groupService.RegisterAsync(_mapper.Map<GroupDto>(group));

            return Ok();
        }

        [Authorize("GroupAdministrator")]
        [HttpDelete("delete-group/{Id}")]
        public async Task<IActionResult> DeleteGroup([Range(1, long.MaxValue)] long Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _groupService.DeleteAsync(Id);

            return Ok();
        }

        [Authorize("GroupAdministrator")]
        [HttpPost("update-group")]
        public async Task<IActionResult> UpdateGroup([FromForm] GroupVM group)
        {
            await _groupService.UpdateAsync(group.Id, _mapper.Map<GroupDto>(group));

            return Ok();
        }
    }
}
