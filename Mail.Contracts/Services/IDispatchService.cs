using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail.Contracts.Services
{
    public interface IDispatchService
    {
        Task Add(string textLetter, UserDto[] users);
        Task Status();
    }
}
