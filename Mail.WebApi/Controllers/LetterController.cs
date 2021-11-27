using AutoMapper;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            await _letterService.SendLetterAsync(letter.TextBody, letter.TextSubject, letter.UsersId);

            return Ok();
        }

        [HttpGet("status-letter")]
        public IActionResult statusLetter()
        {
            var result = _letterService.TakesPercentageCompletion();

            return Ok(result);
        }

        [HttpGet("get-history-lette/{id}")]
        public async Task<IActionResult> StatusLetterByUserId([Range(1, long.MaxValue)] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dispatches = await _letterService.StatusLetterByUserIdAsync(id);

            var result = _mapper.Map<List<LetterStatusVM>>(dispatches);

            return Ok(result);
        }

        [HttpGet("get-lette/{id}")]
        public async Task<IActionResult> LetterByHistoryLetteId([Range(1, long.MaxValue)] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dispatches = await _letterService.GetByIdAsync(id);

            var result = _mapper.Map<LetterVM>(dispatches);

            return Ok(result);
        }
    }
}
