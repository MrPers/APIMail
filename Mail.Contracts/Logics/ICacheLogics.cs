using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Logics
{
    public interface ICacheLogics
    {
        void SaveValueInCache(ICollection<LetterStatusDto> unsentsLettersStatus);
        void SetsKeyValueInCache(long key, long value);
        long GetsKeyValueInCache(long key);
        void CleanCache();
    }
}
