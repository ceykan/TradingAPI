using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradingAPI.Core.Caching.Provider;

namespace TradingAPI.Core.Caching
{
    namespace Pars.Core.Caching
    {
        public class CacheManager : ICacheManager
        {
            private readonly ICacheProvider _provider;
            public CacheManager(ICacheProvider provider)
            {
                _provider = provider;
            }
            public async Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? expiry = null)
            {

                var result = await _provider.GetAsync<T>(key);
                if (result != null)
                    return result;

                if (acquire == null)
                    return default;

                result = await acquire();
                await SetAsync(key, result, expiry);
                return result;
            }
            public async Task<T> GetAsync<T>(string key)
            {
                return await GetAsync<T>(key, null);
            }
            public async Task<bool> SetAsync<T>(string key, T data, TimeSpan? expiry = null)
            {
                return await _provider.SetAsync(key, data, expiry);
            }
            public async Task<bool> RemoveAsync(string key)
            {
                return await _provider.DeleteAsync(key);
            }
        }
    }

}
