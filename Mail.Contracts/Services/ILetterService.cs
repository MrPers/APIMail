using Mail.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface ILetterService
    {
        Task SendLetter(string textBody, string textSubject, ICollection<long> users);
        long TakesFromCachePercentageCompletion();
        Task<ICollection<LetterStatusDto>> StatusLetterByUserId(long id);
        Task<LetterDto> GetById(long id);
    }
}
