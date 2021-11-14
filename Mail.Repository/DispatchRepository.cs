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
    public class DispatchRepository : BaseRepository<Dispatch, DispatchDto, long>, IDispatchRepository
    {
        public DispatchRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<DispatchDto>> findAllById(long Id)
        {
            var result = await _context.Dispatchs.Where(p => p.UserId == Id).ToListAsync();

            return _mapper.Map<List<DispatchDto>>(result);
        }
    }
}