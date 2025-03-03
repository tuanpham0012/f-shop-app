
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Ardalis.GuardClauses;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ShopAppApi.Repositories.RedisCache
{
    public class RedisCache : IRedisCache
    {
        private readonly IDistributedCache _cache;

        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = null,
            WriteIndented = true,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private readonly TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

        public RedisCache(IDistributedCache cache)
        {
            _cache = cache;
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public async Task<T?> GetByKeyAsync<T>(string key)
        {
            var cachedItem = await _cache.GetStringAsync(key);
            return cachedItem != null ? JsonConvert.DeserializeObject<T>(cachedItem) : default(T);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            var val = _cache.GetString(key);
            value = default;
            if (val == null) return false;
            value = JsonConvert.DeserializeObject<T>(val);
            return true;
        }

        public async Task<T?> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, int? time = null)
        {

            if (this.TryGetValue(key, out T? value) && value is not null)
            {
                return value;
            }

            var item = await factory();
            await _cache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(item),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = time != null ? TimeSpan.FromMinutes((double)time) : _cacheExpiry }
            );
            Console.WriteLine("🔄 Item added to cache.");
            return item;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveByPrefix(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}