using Mail.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface ILetterService
    {
        Task SendLetterAsync(string textBody, string textSubject, ICollection<long> users);
        long TakesPercentageCompletion();
        Task<ICollection<LetterStatusDto>> StatusLetterByUserIdAsync(long id);
        Task<LetterDto> GetByIdAsync(long id);
    }
}
