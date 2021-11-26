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
        Task SendLetters(ICollection<LetterStatusDto> dispatchDtos, MailMessage message, SmtpClient client);
        SmtpClient CreationClint();
        Task SaveLetter(string textBody, string textSubject, ICollection<long> usersId);
        Task<MailMessage> CreatMessage(long letterId);
    }
}
