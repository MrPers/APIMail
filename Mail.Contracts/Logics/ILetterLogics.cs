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
        Task<int> SendingLetters(ICollection<LetterStatusDto> dispatchDtos, MailMessage message, SmtpClient client, int lettersCount, int percentageCompletion);
        SmtpClient CreationClint();
        Task SavingRecordCreatingLetter(string textBody, string textSubject, ICollection<long> usersId);
        Task<MailMessage> CreationMessage(long letterId);
    }
}
