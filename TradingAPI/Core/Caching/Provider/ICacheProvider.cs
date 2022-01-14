using System;
using System.Threading.Tasks;

namespace TradingAPI.Core.Caching.Provider
{
    public interface ICacheProvider : IDisposable
    {
       public Task ClearAsync();
       public Task<bool> DeleteAsync(string key);
       public Task<bool> ExistsAsync(string key);
       public Task<bool> SetAsync<T>(string key, T obj, TimeSpan? expiry);
       public Task<T> GetAsync<T>(string key);

    }
}
