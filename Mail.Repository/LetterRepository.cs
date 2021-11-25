using AutoMapper;
using Mail.Contracts.Repo;
using Mail.DB;
using Mail.DB.Models;
using Mail.DTO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mail.Repository
{
    public class LetterRepository : BaseRepository<Letter, LetterDto, long>, ILetterRepository
    {
        public LetterRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
        }

    }
}