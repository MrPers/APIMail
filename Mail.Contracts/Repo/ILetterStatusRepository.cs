using System.Collections.Generic;
using System.Threading.Tasks;
using Mail.DB.Models;
using Mail.DTO.Models;

namespace Mail.Contracts.Repo
{
    public interface ILetterStatusRepository : IBaseRepository<LetterUser, LetterStatusDto, long>
    {
        Task<ICollection<LetterStatusDto>> UnsentLetters();
        Task<ICollection<LetterStatusDto>> FindAllById(long Id);
    }
}
