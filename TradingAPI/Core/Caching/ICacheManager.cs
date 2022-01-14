using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradingAPI.Core.Caching
{
    public interface ICacheManager
    {
        public interface ICacheManager : IDisposable
        {
            public Task<T> GetAsync<T>(string key, Func<Task<T>> acquire, TimeSpan? expiry = null);
            public Task<T> GetAsync<T>(string key);
            public Task<bool> SetAsync<T>(string key, T data, TimeSpan? expiry = null);           
            public Task<bool> RemoveAsync(string key);
        }
    }
}
