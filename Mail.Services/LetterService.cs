using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.Repository;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Services
{
    public class LetterService : ILetterService
    {
        private ILetterRepository _dispatchRepository;
        private IMemoryCache _cache;
        private IConfiguration _configuration;
        private ILogger<LetterService> _logger;
        private readonly int keyWithPercentDispatchExecution;

        public LetterService(
            IConfiguration configuration,
            IMemoryCache cache, 
            ILetterRepository dispatchRepository,
            ILogger<LetterService> logger
            )
        {
            keyWithPercentDispatchExecution = 1;
            _cache = cache;
            _dispatchRepository = dispatchRepository;
            _configuration = configuration;
            _logger = logger;
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

        public async Task Add(string textBody, string textSubject, UserDto[] users)
        {
            try
            {
                MailMessage message = CreationMessage(textBody, textSubject);

                SmtpClient client = CreationClint();

                LetterStatusDto[] dispatchDtos = await creationRecordsWhomLetterWillSent(users);

                await sendingLetters(users, dispatchDtos, message, client);

            }
            catch (Exception e)
            {
                _logger.LogInformation(e.Message);
            }
        }

        private async Task<LetterStatusDto[]> creationRecordsWhomLetterWillSent(UserDto[] users)
        {
            LetterStatusDto[] dispatchDtos = new LetterStatusDto[users.Length];
            _cache.Remove(keyWithPercentDispatchExecution);

            for (int i = 0; i < users.Length; i++)
            {
                dispatchDtos[i] = new LetterStatusDto()
                {
                    UserId = users[i].Id,
                    DepartureDate = DateTime.Now
                };

                dispatchDtos[i].Id = await _dispatchRepository.Add(dispatchDtos[i], false);
            }

            await _dispatchRepository.SaveChanges();

            return dispatchDtos;
        }

        private SmtpClient CreationClint()
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

        private async Task sendingLetters(UserDto[] users, LetterStatusDto[] dispatchDtos, MailMessage message, SmtpClient client)
        {
            int timeSpanFromSeconds = 6;
            Random rand = new Random();

            for (int i = 0; i < users.Length; i++)
            {
                message.To.Add(users[i].Email);

                client.Send(message);

                dispatchDtos[i].Status = true;

                await _dispatchRepository.Update(dispatchDtos[i].Id, dispatchDtos[i]);

                if (users.Length > 1)
                {
                    _cache.Set(keyWithPercentDispatchExecution, 100 * (i + 1) / users.Length, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(timeSpanFromSeconds)
                    });

                    System.Threading.Thread.Sleep(rand.Next(1, 3) * 1000);
                }
            }
        }

        private MailMessage CreationMessage(string textBody, string textSubject)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(Convert.ToString(_configuration["MailConnection:address"]), Convert.ToString(_configuration["MailConnection:name"]));
            message.Subject = textSubject;
            message.Body = textBody;
            return message;
        }

        public async Task<List<LetterStatusDto>> GetDispatches(long id)
        {
            if (id < 1)
            {
                throw new ArgumentException(nameof(id));
            }

            var result = await _dispatchRepository.findAllById(id);

            return result;
        }
    }
}
