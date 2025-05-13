using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Mima.Application.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.TryGetValue(key, out T value) ? value : default;
        }

        public void Set<T>(string key, T value)
        {
            _cache.Set(key, value);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Exists(string key)
        {
            return _cache.TryGetValue(key, out _);
        }
    }
}
