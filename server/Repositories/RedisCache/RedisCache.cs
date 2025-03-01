
using StackExchange.Redis;

namespace ShopAppApi.Repositories.RedisCache
{
    public class RedisCache : IRedisCache
    {
        private readonly IConnectionMultiplexer _redis;

        private readonly IDatabase _database;

        public RedisCache(IConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }


        public void Clear()
        {
            throw new NotImplementedException();
        }

        public TItem? GetByKey<TItem>(string key)
        {
            throw new NotImplementedException();
        }

        public TItem? GetOrCreate<TItem>(string key, Func<TItem> factory)
        {
            throw new NotImplementedException();
        }

        public TItem? GetOrCreate<TItem>(string key, Func<TItem> factory, int time)
        {
            var result = _database.Has(key);
        }

        public Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory)
        {
            throw new NotImplementedException();
        }

        public Task<TItem?> GetOrCreateAsync<TItem>(string key, Func<Task<TItem>> factory, int time)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPrefix(string pattern)
        {
            throw new NotImplementedException();
        }

        public TItem SetValue<TItem>(string key, TItem value)
        {
            throw new NotImplementedException();
        }

        public TItem SetValue<TItem>(string key, TItem value, int time)
        {
            throw new NotImplementedException();
        }
    }
}