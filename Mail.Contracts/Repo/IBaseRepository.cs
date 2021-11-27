using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Repo
{
    public interface IBaseRepository<TTable, TDto, TId> where TTable : IBaseEntity<TId>
    {
        Task<ICollection<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(TId Id);
        Task<TId> GetIdAddAsync(IEnumerable<TDto> Dto);
        Task<TId> GetIDAddAsync(TDto Dto);
        Task UpdateAsync(TId Id, TDto meaning); 
        Task DeleteAsync(TId Id);
        Task SaveChangesAsync();
    }
}
