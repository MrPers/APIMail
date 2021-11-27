using Mail.DTO.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface IUserService
    {
        Task RegisterAsync(UserDto user);
        Task<ICollection<UserDto>> GetAllAsync();
        Task UpdateAsync(long Id, UserDto user);
        Task DeleteAsync(long Id);
        Task SubscriptionToGroupsAsync(long IdGroup, ICollection<long> IdUsers);
        Task UnsubscriptionToGroupsAsync(long IdGroup, ICollection<long> IdUsers);
    }
}
