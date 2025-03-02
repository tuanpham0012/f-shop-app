
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

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

        public async Task<TItem?> GetByKeyAsync<TItem>(string key)
        {
            var cachedItem = await _cache.GetStringAsync(key);
            return JsonConvert.DeserializeObject<TItem>(cachedItem);
        }

        public async Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory)
        {
            var cachedItem = await _cache.GetStringAsync(key);
            if (cachedItem != null)
            {
                Console.WriteLine("✅ Item retrieved from cache!");
                return JsonConvert.DeserializeObject<TItem>(cachedItem);
            }

            var item = await factory();
            await _cache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(item),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = _cacheExpiry }
            );
            Console.WriteLine("🔄 Item added to cache.");
            return item;
        }

        public async Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory, int time)
        {
            var cachedItem = await _cache.GetStringAsync(key);
            if (cachedItem != null)
            {
                Console.WriteLine("✅ Item retrieved from cache!");
                return JsonConvert.DeserializeObject<TItem>(cachedItem);
            }

            var item = await factory();
            await _cache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(item),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(time) }
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