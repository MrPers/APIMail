using Mail.Contracts.Logics;
using Mail.Contracts.Repo;
using Mail.DTO.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Business.Logics
{
    public class LetterLogics : ILetterLogics
    {
        private readonly int keyWithPercentDispatchExecution;
        private ILetterRepository _letterRepository;
        private ILetterStatusRepository _letterStatusRepository;
        private IUserRepository _userRepository;
        private IMemoryCache _cache;
        private IConfiguration _configuration;

        public LetterLogics(
            ILetterStatusRepository letterStatusRepository,
            IMemoryCache cache,
            ILetterRepository dispatchRepository,
            IUserRepository userRepository,
            IConfiguration configuration = null
        )
        {
            _cache = cache;
            _letterStatusRepository = letterStatusRepository;
            _userRepository = userRepository;
            _letterRepository = dispatchRepository;
            _configuration = configuration;
            keyWithPercentDispatchExecution = Convert.ToInt32(_configuration["MailConnection:keyWithPercentDispatchExecution"]);
        }

        public async Task SavingRecordCreatingLetter(string textBody, string textSubject, ICollection<long> usersId)
        {
            _cache.Remove(keyWithPercentDispatchExecution);

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
            SmtpClient client = new SmtpClient(Convert.ToString(_configuration["MailConnection:smtpClien"]));
            client.Credentials = new NetworkCredential(
                Convert.ToString(_configuration["MailConnection:address"]),
                Convert.ToString(_configuration["MailConnection:pasword"])
                );
            client.Port = Convert.ToInt32(_configuration["MailConnection:port"]);
            client.EnableSsl = true;
            return client;
        }

        public async Task<MailMessage> CreationMessage(long letterId)
        {
            var letter = await _letterRepository.GetById(letterId);

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(Convert.ToString(_configuration["MailConnection:address"]), Convert.ToString(_configuration["MailConnection:name"]));
            message.Subject = letter.TextSubject;
            message.Body = letter.TextBody;
            return message;
        }
        
        public async Task<int> SendingLetters(ICollection<LetterStatusDto> lettersStatus, MailMessage message, SmtpClient client, int lettersCount, int percentageCompletion)
        {
            int timeSpanFromSeconds = 6;
            Random rand = new Random();

            foreach (var item in lettersStatus)
            {
                var user = await _userRepository.GetById(item.UserId);

                message.To.Add(user.Email);

                client.Send(message);

                if (lettersStatus.Count > 1)
                {
                    _cache.Set(keyWithPercentDispatchExecution,
                        100 * ++percentageCompletion / lettersStatus.Count, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(timeSpanFromSeconds)
                        });

                    System.Threading.Thread.Sleep(rand.Next(1, 3) * 1000);
                }

                item.Status = true;
                item.DepartureDate = DateTime.Now;

                await _letterStatusRepository.Update(item.Id, item);
            }

            return percentageCompletion;
        }
    }
}
