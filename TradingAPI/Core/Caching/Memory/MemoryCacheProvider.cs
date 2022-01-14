using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using TradingAPI.Core.Caching.Provider;

namespace TradingAPI.Core.Caching.Memory
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public IMemoryCache Cache { get; set; }

        public MemoryCacheProvider()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }
        public Task<bool> DeleteAsync(string key)
        {
            Cache.Remove(key);
            return Task.FromResult(true);
        }
        public Task<bool> ExistsAsync(string key)
        {
            var deneme = Task.FromResult(Cache.TryGetValue(key, out _));
            return deneme;
        }
        public Task<bool> SetAsync<T>(string key, T obj, TimeSpan? expiry)
        {
            if (expiry == null)
            {
                Cache.Set(key, obj);
            }
            else
            {
                Cache.Set(key, obj, (TimeSpan)expiry);
            }

            return Task.FromResult(true);
        }
        public Task<T> GetAsync<T>(string key)
        {
            return Task.FromResult(Cache.Get<T>(key));
        }

        public async Task ClearAsync()
        {
            await Task.CompletedTask;
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        public void Dispose()
        {
            Cache?.Dispose();
        }
    }
}
