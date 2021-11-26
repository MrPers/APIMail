using Mail.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterAsync(UserDto user);
        Task<ICollection<UserDto>> GetAll();
        Task Update(long Id, UserDto user);
        Task Delete(long Id);
        Task AddInGroup(long IdGroup, ICollection<long> IdUsers);
        Task DeleteFromGroup(long IdGroup, ICollection<long> IdUsers);
    }
}
