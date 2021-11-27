using Mail.Contracts.Logics;
using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace Mail.Business.Services
{
    public class LetterService : ILetterService
    {
        private IOptions<MySettingsModel> _appSettings;
        private ILetterRepository _letterRepository;
        private ILetterStatusRepository _letterStatusRepository;
        private ILogger<LetterService> _logger;
        private ILetterLogics _letterLogics;
        private ICacheLogics _cacheLogics;

        public LetterService(
            IOptions<MySettingsModel> appSettings,
            ILetterStatusRepository letterStatusRepository,
            ILetterRepository dispatchRepository,
            ILogger<LetterService> logger,
            ILetterLogics letterLogics,
            ICacheLogics cacheLogics
        )
        {
            _cacheLogics = cacheLogics;
            _appSettings = appSettings;
            _letterStatusRepository = letterStatusRepository;
            _letterRepository = dispatchRepository;
            _logger = logger;
            _letterLogics = letterLogics;
        }

        public long TakesPercentageCompletion()
        {
            long percentageСompletion = _cacheLogics.GetsKeyValueInCache(_appSettings.Value.KeyWithPercentDispatchExecution);

            return percentageСompletion;
        }

        public async Task SendLetterAsync(string textBody, string textSubject, ICollection<long> usersId)
        {
            if (usersId.Count < 1)
            {
                throw new ArgumentException(nameof(usersId));
            }

            try
            {
                MailMessage message;

                ICollection<LetterStatusDto> lettersStatus;

                _cacheLogics.CleanCache();

                await _letterLogics.SaveLetterAsync(textBody, textSubject, usersId);

                SmtpClient client = _letterLogics.CreationClint();

                var unsentsLettersStatus = await _letterStatusRepository.GetLettersStrtusUnsentLettersAsync();

                var shapeLettersId = unsentsLettersStatus.Select(x => x.LetterId).Distinct();

                _cacheLogics.SaveValueInCache(unsentsLettersStatus);

                foreach (var item in shapeLettersId)
                {
                    message = await _letterLogics.CreatMessageAsync(item);

                    lettersStatus = unsentsLettersStatus.Where(x => x.LetterId == item).ToList();

                    await _letterLogics.SendLettersAsync(
                        lettersStatus,
                        message,
                        client
                        );
                }
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
        }

        public async Task<ICollection<LetterStatusDto>> StatusLetterByUserIdAsync([Range(1, long.MaxValue)] long id)
        {
            var result = await _letterStatusRepository.GetLettersStatusFindAllByIdUserIdASync(id);

            return result;
        }

        public async Task<LetterDto> GetByIdAsync([Range(1, long.MaxValue)] long id)
        {
            var result = await _letterRepository.GetByIdAsync(id);

            return result;
        }

    }
}
