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
        private readonly ILetterService _letterService;
        private readonly IMapper _mapper;
        private ILogger<LetterController> _logger;

        public LetterController(
            ILetterService dispatchService,
            ILogger<LetterController> logger,
            IMapper mapper
        )
        {
            _letterService = dispatchService;
            _mapper = mapper;
            _logger = logger;
            _logger.LogInformation("Initialization");
        }

        [HttpPost("send-letter")]
        public async Task<IActionResult> SendLetter(LetterVM letter)
        {
            await _letterService.SendLetter(letter.TextBody, letter.TextSubject, letter.UsersId);

            return Ok();
        }

        [HttpGet("status-letter")]
        public IActionResult statusLetter()
        {            
            var result = _letterService.Status();

            return Ok(result);
        }

        [HttpGet("get-history-lette/{id}")]
        public async Task<IActionResult> StatusLetterByUserId(long id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var dispatches = await _letterService.StatusLetterByUserId(id);

            var result = _mapper.Map<List<LetterStatusVM>>(dispatches);

            return Ok(result);
        }

        [HttpGet("get-lette/{id}")]
        public async Task<IActionResult> LetterByHistoryLetteId(long id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var dispatches = await _letterService.GetById(id);

            var result = _mapper.Map<LetterVM>(dispatches);

            return Ok(result);
        }
    }
}
