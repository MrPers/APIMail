using Mail.Contracts.Logics;
using Mail.Contracts.Repo;
using Mail.DTO.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mail.Business.Logics
{
    public class CacheLogics : ICacheLogics
    {
        private IOptions<MySettingsModel> _appSettings;
        const int TimeSpanFromSeconds = 6;
        private IMemoryCache _cache;
        private ILetterRepository _letterRepository;

        public CacheLogics(
            ILetterRepository letterRepository,
            IOptions<MySettingsModel> appSettings, 
            IMemoryCache cache
        )
        {
            _appSettings = appSettings;
            _cache = cache;
            _letterRepository = letterRepository;
        }

        public void SaveValueInCache(ICollection<LetterStatusDto> unsentsLettersStatus)
        {
            SetsKeyValueInCache(_appSettings.Value.KeyWithPercentDispatchExecution, 0);

            SetsKeyValueInCache(_appSettings.Value.KeyWithWholeDispatchExecution, unsentsLettersStatus.Count());

            SetsKeyValueInCache(_appSettings.Value.KeyWithPercentageCompletion, 0);
        }

        public void SetsKeyValueInCache(long key, long value) 
        {
            _cache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(TimeSpanFromSeconds)
            });
        }

        public long GetsKeyValueInCache(long key)
        {// separate
            long percentageСompletion = 0; // remove

            _cache.TryGetValue(key, out percentageСompletion);

            return percentageСompletion;
        }

        public void CleanCache()
        {
            _cache.Remove(_appSettings.Value.KeyWithPercentDispatchExecution);
            _cache.Remove(_appSettings.Value.KeyWithWholeDispatchExecution);
            _cache.Remove(_appSettings.Value.KeyWithPercentageCompletion);
        }


    }
}
