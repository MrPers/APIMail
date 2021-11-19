using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Repo
{
    public interface IBaseRepository<TTable, TDto, TId> where TTable : IBaseEntity<TId>
    {
        Task<List<TDto>> GetAll();
        Task<TDto> GetById(TId Id);
        Task<TId> Add(TDto Dto, bool status = true);
        Task Update(TId Id, TDto meaning, bool status = true); 
        Task Delete(TId Id);
        Task SaveChanges();
    }
}
