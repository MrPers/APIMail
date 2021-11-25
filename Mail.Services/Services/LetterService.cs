using Mail.Business.Logics;
using Mail.Contracts.Logics;
using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mail.Business.Services
{
    public class LetterService : ILetterService
    {
        private int keyWithPercentDispatchExecution;
        private ILetterRepository _letterRepository;
        private ILetterStatusRepository _letterStatusRepository;
        private IMemoryCache _cache;
        private IConfiguration _configuration;
        private ILogger<LetterService> _logger;
        private ILetterLogics _letterLogics;

        public LetterService(
            IMemoryCache cache,
            ILetterStatusRepository letterStatusRepository,
            ILetterRepository dispatchRepository,
            ILogger<LetterService> logger,
            ILetterLogics letterLogics,
            IConfiguration configuration = null
            )
        {
            _letterStatusRepository = letterStatusRepository;
            _cache = cache;
            _letterRepository = dispatchRepository;
            _configuration = configuration;
            _logger = logger;
            _letterLogics = letterLogics;
            keyWithPercentDispatchExecution = Convert.ToInt32(_configuration["MailConnection:keyWithPercentDispatchExecution"]);
            //keyWithPercentDispatchExecution = 1;
        }

        public int Status()
        {
            int result = 0;

            if (_cache.TryGetValue(keyWithPercentDispatchExecution, out int percentageСompletion))
            {
                result = percentageСompletion;
            }

            return result;
        }

        public async Task SendLetter(string textBody, string textSubject, ICollection<long> usersId)
        {
            if (usersId == null)
            {
                throw new ArgumentNullException(nameof(usersId));
            }
            if (usersId.Count < 1)
            {
                throw new ArgumentException(nameof(usersId));
            }

            try
            {
                MailMessage message;
                ICollection<LetterStatusDto> lettersStatusFiltre;

                await _letterLogics.SavingRecordCreatingLetter(textBody, textSubject, usersId);

                SmtpClient client = _letterLogics.CreationClint();

                var lettersStatus = await _letterStatusRepository.UnsentLetters();

                var letters = lettersStatus.Select(x => x.LetterId).Distinct();

                int lettersCount = lettersStatus.Count();

                int percentageCompletion = 0;

                foreach (var item in letters)
                {
                    message = await _letterLogics.CreationMessage(item);
                    lettersStatusFiltre = lettersStatus.Where(x => x.LetterId == item).ToList();

                    percentageCompletion = await _letterLogics.SendingLetters(
                        lettersStatusFiltre,
                        message,
                        client,
                        lettersCount,
                        percentageCompletion
                        );;
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
        }

        public async Task<ICollection<LetterStatusDto>> StatusLetterByUserId(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            var result = await _letterStatusRepository.FindAllById(id);

            return result;
        }

        public async Task<LetterDto> GetById(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            var result = await _letterRepository.GetById(id);

            return result;
        }

    }
}
