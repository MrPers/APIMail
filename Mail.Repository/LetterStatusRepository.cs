using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class LetterStatusRepository : BaseRepository<LetterUser, LetterStatusDto, long>, ILetterStatusRepository
    {
        public LetterStatusRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ICollection<LetterStatusDto>> UnsentLetters()
        {
            var time = await _context.Set<LetterUser>()
                .Where(x => x.Status == false)
                .ToListAsync();

            return _mapper.Map<ICollection<LetterStatusDto>>(time);
        }

        public async Task<ICollection<LetterStatusDto>> FindAllById(long Id)
        {
            if (Id < 1)
            {
                throw new ArgumentException(nameof(Id));
            }

            var result = await _context.Set<LetterUser>()
                .Where(p => p.UserId == Id)
                .ToListAsync();

            return _mapper.Map<List<LetterStatusDto>>(result);
        }

    }
}
