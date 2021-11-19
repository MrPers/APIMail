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
        Task Add(string textBody, string textSubject, UserDto[] users);
        int Status();
        Task<List<LetterStatusDto>> GetDispatches(long id);
    }
}
