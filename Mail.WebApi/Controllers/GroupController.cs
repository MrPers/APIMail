using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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

        [HttpGet("get-groups-all")]
        public async Task<IActionResult> GetGroupAll()
        {
            var groups = await _groupService.GetAll();
            IActionResult result = groups == null ? NotFound() : Ok(_mapper.Map<List<GroupVM>>(groups));

            return result;
        }

        [HttpPost("regist-group")]
        public async Task<IActionResult> RegisterGroup([FromForm] GroupVM group)
        {
            await _groupService.RegisterAsync(_mapper.Map<GroupDto>(group));

            return Ok();
        }

        [HttpDelete("delete-group/{Id}")]
        public async Task<IActionResult> DeleteGroup(long Id)
        {
            if (Id < 1)
            {
                return BadRequest();
            }

            await _groupService.Delete(Id);

            return Ok();
        }

        [HttpPost("update-group")]
        public async Task<IActionResult> UpdateGroup([FromForm] GroupVM group)
        {
            await _groupService.Update(group.Id, _mapper.Map<GroupDto>(group));

            return Ok();
        }
    }
}
