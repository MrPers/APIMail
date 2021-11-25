using Mail.DB.Models;
using Mail.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Contracts.Repo
{
    public interface ILetterRepository : IBaseRepository<Letter, LetterDto, long>
    {
    }
}
