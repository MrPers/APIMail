using Mail.Contracts.Repo;
using Mail.Contracts.Services;
using Mail.DTO.Models;
using Mail.Repository;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Services
{
    public class DispatchService : IDispatchService
    {
        private IDispatchRepository _dispatchRepository;
        private IMemoryCache _cache;
        public DispatchService(IMemoryCache cache, IDispatchRepository dispatchRepository)
        {
            _cache = cache;
            _dispatchRepository = dispatchRepository;
        }

        public async Task<int> Status()
        {
            int result = 0;
            if (_cache.TryGetValue(1, out int percentageСompletion))
            {
                result = percentageСompletion;
            }
            return result;
        }

        public async Task Add(string textBody, string textSubject, UserDto[] users)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                Random rand = new Random();
                DispatchDto[] dispatchDtos = new DispatchDto[users.Length];

                message.IsBodyHtml = true;
                message.From = new MailAddress("iamanton45@gmail.com", "Test");
                message.Subject = textSubject;
                message.Body = textBody;

                client.Credentials = new NetworkCredential("iamanton45@gmail.com", "1878$zif2112");
                client.Port = 587;
                client.EnableSsl = true;

                _cache.Remove(1);
                for (int i = 0; i < users.Length; i++)
                {
                    dispatchDtos[i] = new DispatchDto()
                    {
                        UserId = users[i].Id,
                        DepartureDate = DateTime.Now
                    };

                    dispatchDtos[i].Id = await _dispatchRepository.Add(dispatchDtos[i]);
                }

                for (int i = 0; i < users.Length; i++)
                {
                    message.To.Add(users[i].Email);

                    client.Send(message);

                    dispatchDtos[i].Status = true;

                    await _dispatchRepository.Update(dispatchDtos[i].Id, dispatchDtos[i]);

                    await _dispatchRepository.SaveChanges();

                    if (users.Length > 1)
                    {
                        _cache.Set(1, 100 * (i + 1) / users.Length, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(6)
                        });

                        System.Threading.Thread.Sleep(rand.Next(1, 3) * 1000);
                    }
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }

        public async Task<List<DispatchDto>> GetDispatches(long id)
        {
            //return await _dispatchRepository.GetAll();
            var result = await _dispatchRepository.findAllById(id);

            return result;
        }
    }
}
