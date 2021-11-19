using AutoMapper;
using Mail.Contracts;
using Mail.Contracts.Repo;
using Mail.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public abstract class BaseRepository<TTable, TDto, TId> : IBaseRepository<TTable, TDto, TId> where TTable : class, IBaseEntity<TId>
    {
        protected readonly DataContext _context;
        protected readonly IMapper _mapper;
        public BaseRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TId> Add(TDto Dto, bool status = true)
        {
            var time = _mapper.Map<TTable>(Dto);
            await _context.Set<TTable>().AddAsync(time);

            if (status)
            {
                await _context.SaveChangesAsync();
            }
            return time.Id;
        }

        public async Task Update(TId Id, TDto meaning, bool status = true)
        {
            var result = _context.Set<TTable>().Find(Id);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(_mapper.Map<TTable>(meaning));
                
                if (status)
                {
                    await _context.SaveChangesAsync();
                }
            }

        }

        public async Task Delete(TId Id)
        {
            _context.Entry<TTable>(_context.Set<TTable>().Find(Id)).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TDto>> GetAll()
        {
            var dbItems = await _context.Set<TTable>().ToListAsync();
            return _mapper.Map<List<TDto>>(dbItems);
        }

        public async Task<TDto> GetById(TId Id)
        {
            var dbItem = await _context.Set<TTable>().FindAsync(Id);
            return _mapper.Map<TDto>(dbItem);
        }
        
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
