using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Logics
{
    public interface ILetterLogics
    {
        Task SendLettersAsync(ICollection<LetterStatusDto> dispatchDtos, MailMessage message, SmtpClient client);
        SmtpClient CreationClint();
        Task SaveLetterAsync(string textBody, string textSubject, ICollection<long> usersId);
        Task<MailMessage> CreatMessageAsync(long letterId);
    }
}
