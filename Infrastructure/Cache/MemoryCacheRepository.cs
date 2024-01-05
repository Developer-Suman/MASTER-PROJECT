using Domain.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Cache
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            
        }
        public async Task<T?> GetAsync<T>(string cacheKey)
        {
            return await Task.FromResult(_memoryCache.TryGetValue(cacheKey, out T? value)? value : default(T));
        }

        public async Task RemoveAsync(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
            await Task.CompletedTask;
        }

        public async Task SetAsync<T>(string cacheKey, T value, MemoryCacheEntryOptions options)
        {
            if(value is not null)
            {
                _memoryCache.Set(cacheKey, value, options);
            }
            await Task.CompletedTask;
        }
    }
}
