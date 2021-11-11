using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class DispatchRepository : BaseRepository<Dispatch, DispatchDto, long>, IDispatchRepository
    {
        public DispatchRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}