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

        public async Task Add(TDto Dto)
        {
            await _context.Set<TTable>().AddAsync(_mapper.Map<TTable>(Dto));
            await _context.SaveChangesAsync();
        }

        public async Task Apdate(TId Id)
        {
            _context.Entry<TTable>(_context.Set<TTable>().Find(Id)).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TId Id)
        {
            _context.Entry<TTable>(_context.Set<TTable>().Find(Id)).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<TDto>> GetAll()
        {
            var dbItems = _context.Set<TTable>().ToList();
            return _mapper.Map<List<TDto>>(dbItems);
        }

        public async Task<TDto> GetById(TId Id)
        {
            var dbItem = _context.Set<TTable>().Find(Id);
            return _mapper.Map<TDto>(dbItem);
        }

    }
}
