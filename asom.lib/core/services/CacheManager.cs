using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace asom.lib.core.services
{
    public class CacheManager<T> : ICacheManager<T>
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<CacheManager<T>> _logger;

        public CacheManager(IDistributedCache cache, ILogger<CacheManager<T>> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<T> Get(string cacheKey)
        {
            if (string.IsNullOrEmpty(cacheKey)) return default(T);
            cacheKey = formatCacheKey(cacheKey);
            _logger.LogInformation($"Get Cache called with Key :{cacheKey} of type {typeof(T)}");

            var result = await _cache.GetAsync(cacheKey);
            if (result == null)
            {
                _logger.LogInformation($"Item not found in Cache. with Key :{cacheKey}");
                return default;
            }

            var ret = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(result));
            _logger.LogInformation($"Item Found in Cache with Key :{cacheKey}");

            return ret;
        }

        public async Task<bool> Clear(string cacheKey)
        {
            cacheKey = formatCacheKey(cacheKey);
            var result = await Get(cacheKey);
            if (result == null)
            {
                return true;
            }

            await _cache.RemoveAsync(cacheKey);

            return true;
        }

        public async Task Set(string cacheKey, T item, TimeSpan duration, bool withAbsoluteExpiring = false)
        {
            if (string.IsNullOrEmpty(cacheKey) || item == null)
                return;
            cacheKey = formatCacheKey(cacheKey);
            var serializeObject = JsonConvert.SerializeObject(item);

            var options = new DistributedCacheEntryOptions().SetSlidingExpiration(duration);

            if (withAbsoluteExpiring)
                options = options.SetAbsoluteExpiration(duration);

            _logger.LogInformation(
                $"Set Cache item called with Key :{cacheKey}, for type {(typeof(T))}. Duration :{duration}, with absolute expiration set to : {withAbsoluteExpiring}");

            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(serializeObject), options);
        }

        string formatCacheKey(string cacheKey) =>
            cacheKey.Trim().ToLower();

        public async Task Set(string cacheKey, T item, ushort durationInSeconds)
        {
            await Set(cacheKey, item, TimeSpan.FromSeconds(durationInSeconds));
        }
    }

    public class CacheManagerFactory : ICacheManagerFactory
    {
        private readonly IServiceProvider _provider;

        public CacheManagerFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        ICacheManager<T> ICacheManagerFactory.Create<T>()
        {
            if (_provider == null)
                throw new ArgumentNullException(nameof(_provider));
            return _provider.GetRequiredService<ICacheManager<T>>();

        }
    }

    /// <summary>
    /// Create or get existing Instance of an ICacheManager of T.
    /// <returns>return an instance of CacheManger of T container for caching data</returns>
    /// </summary>
    public interface ICacheManagerFactory
    {
        ICacheManager<T> Create<T>();
    }
}
