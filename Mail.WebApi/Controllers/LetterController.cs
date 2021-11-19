using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.WebApi.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LetterController : ControllerBase
    {
        private readonly ILetterService _dispatchService;
        private readonly IMapper _mapper;
        private ILogger<LetterController> _logger;

        public LetterController(
            ILetterService dispatchService,
            ILogger<LetterController> logger,
            IMapper mapper
        )
        {
            _dispatchService = dispatchService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Initialization");
        }

        [HttpPost("send-letter")]
        public async Task<IActionResult> SendLetter(LetterVM letter)
        {
            await _dispatchService.Add(letter.TextBody, letter.TextSubject, _mapper.Map<UserDto[]>(letter.Users));

            return Ok();
        }

        [HttpGet("status-letter")]
        public IActionResult statusLetter()
        {            
            var result = _dispatchService.Status();

            return Ok(result);
        }

        [HttpGet("get-history-lette/{id}")]
        public async Task<IActionResult> StatusLetterById(long id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var dispatches = await _dispatchService.GetDispatches(id);

            var result = _mapper.Map<List<LetterStatusVM>>(dispatches);

            return Ok(result);
        }
    }
}
