using System;
using System.Threading.Tasks;

namespace asom.lib.core.services
{
    public interface ICacheManager<T>
    {
        Task<T> Get(string key);
        Task Set(string cacheKey, T item, TimeSpan duration, bool withAbsoluteExpiring = false);
        Task Set(string cacheKey, T item, ushort durationInSeconds);
        Task<bool> Clear(string cacheKey);
    }
}
