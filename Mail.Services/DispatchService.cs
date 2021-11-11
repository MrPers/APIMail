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

        public async Task Status()
        {
            //cache.Set(user.Id, user, new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            //});

            //if (!cache.TryGetValue(id, out user))
            //{
            //    user = await db.Users.FirstOrDefaultAsync(p => p.Id == id);
            //    if (user != null)
            //    {
            //        cache.Set(user.Id, user,
            //        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
            //    }
            //}
        }

        public async Task Add(string textLetter, UserDto[] users)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                Random rand = new Random();
                DispatchDto[] dispatchDtos = new DispatchDto[users.Length];

                message.IsBodyHtml = true;
                message.From = new MailAddress("iamanton45@gmail.com", "Test");
                message.Subject = textLetter;
                message.Body = "Test body";

                client.Credentials = new NetworkCredential("iamanton45@gmail.com", "1878$zif2112");
                client.Port = 587;
                client.EnableSsl = true;

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
                    System.Threading.Thread.Sleep(rand.Next(1, 5) * 1000);

                    message.To.Add(users[i].Email);

                    client.Send(message);

                    dispatchDtos[i].Status = true;

                    await _dispatchRepository.Update(dispatchDtos[i].Id, dispatchDtos[i]);
                }

            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
        }
    }
}
