using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface ILetterService
    {
        Task SendLetter(string textBody, string textSubject, ICollection<long> users);
        int Status();
        Task<ICollection<LetterStatusDto>> StatusLetterByUserId(long id);
        Task<LetterDto> GetById(long id);
    }
}
