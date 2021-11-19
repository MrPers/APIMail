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
    public class LetterRepository : BaseRepository<LetterStatus, LetterStatusDto, long>, ILetterRepository
    {
        public LetterRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<LetterStatusDto>> findAllById(long Id)
        {
            if (Id < 1)
            {
                throw new ArgumentException(nameof(Id));
            }

            var result = await _context.Dispatchs.Where(p => p.UserId == Id).ToListAsync();

            return _mapper.Map<List<LetterStatusDto>>(result);
        }
    }
}