using Mail.Contracts.Logics;
using Mail.Contracts.Repo;
using Mail.DTO.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mail.Business.Logics
{
    public class LetterLogics : ILetterLogics
    {
        private IOptions<MySettingsModel> _appSettings;
        private ILetterRepository _letterRepository;
        private ILetterStatusRepository _letterStatusRepository;
        private IUserRepository _userRepository;
        private ICacheLogics _cacheLogics;

        public LetterLogics(
            ICacheLogics cacheLogics,
            ILetterStatusRepository letterStatusRepository,
            ILetterRepository dispatchRepository,
            IUserRepository userRepository,
            IOptions<MySettingsModel> appSettings
        )
        {
            _cacheLogics = cacheLogics;
            _appSettings = appSettings;
            _letterStatusRepository = letterStatusRepository;
            _userRepository = userRepository;
            _letterRepository = dispatchRepository;
        }

        public async Task SaveLetter(string textBody, string textSubject, ICollection<long> usersId)
        {
            var Letter = new LetterDto()
            {
                TextBody = textBody,
                TextSubject = textSubject,
                DepartureСreation = DateTime.Now,
            };

            var letterId = await _letterRepository.Add(Letter);

            var RangeLetterStatus = usersId.Select(x => new LetterStatusDto()
            {
                UserId = x,
                LetterId = letterId,
                DepartureDate = DateTime.Now
            }).ToList();

            await _letterStatusRepository.Add(RangeLetterStatus);

        }

        public SmtpClient CreationClint()
        {
            SmtpClient client = new SmtpClient(_appSettings.Value.SmtpClien);
            client.Credentials = new NetworkCredential(_appSettings.Value.Address,_appSettings.Value.Pasword);
            client.Port = Convert.ToInt32(_appSettings.Value.Port);
            client.EnableSsl = true;
            return client;
        }

        public async Task<MailMessage> CreatMessage(long letterId)
        {
            LetterDto letter = await _letterRepository.GetById(letterId);

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(_appSettings.Value.Address, _appSettings.Value.Name);
            message.Subject = letter.TextSubject;
            message.Body = letter.TextBody;
            return message;
        }

        public async Task SendLetters(ICollection<LetterStatusDto> lettersStatus, MailMessage message, SmtpClient client)
        {
            Random rand = new Random();
            
            var lettersCount = _cacheLogics.GetsKeyValueInCache(_appSettings.Value.KeyWithWholeDispatchExecution); //rename, rename

            var percentageCompletion = _cacheLogics.GetsKeyValueInCache(_appSettings.Value.KeyWithPercentageCompletion); // rename

            foreach (var item in lettersStatus)
            {
                await SendLetter(message, client, item);

                await ChangSetatusLetterInDatabase(item);

                if (lettersCount > 1)
                {
                    _cacheLogics.SetsKeyValueInCache(_appSettings.Value.KeyWithPercentDispatchExecution, 100 * ++percentageCompletion / lettersCount);

                    System.Threading.Thread.Sleep(rand.Next(1, 3) * 100);
                }
            }

            _cacheLogics.SetsKeyValueInCache(_appSettings.Value.KeyWithPercentageCompletion, percentageCompletion);
        }

        private async Task ChangSetatusLetterInDatabase(LetterStatusDto item)
        {
            item.Status = true;
            item.DepartureDate = DateTime.Now;

            await _letterStatusRepository.Update(item.Id, item);
        }

        private async Task SendLetter(MailMessage message, SmtpClient client, LetterStatusDto item)
        {
            UserDto user = await _userRepository.GetById(item.UserId);

            message.To.Add(user.Email);

            client.Send(message);
        }

    }
}
