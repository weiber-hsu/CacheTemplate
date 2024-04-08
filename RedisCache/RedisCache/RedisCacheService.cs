using StackExchange.Redis;

namespace RedisCache;

public class RedisCacheService(RedisConfig redisConfig): ICacheService
{
    private readonly string _keyPostfix = redisConfig.KeyPostfix;
    private readonly IDatabase _redisDb = redisConfig.GetConnection();

    public void Add<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        throw new NotImplementedException();
    }

    public TValue Get<TValue>(string key)
    {
        throw new NotImplementedException();
    }

    public void Delete(string key)
    {
        throw new NotImplementedException();
    }

    public bool TryGet<TValue>(string key, out TValue? value)
    {
        throw new NotImplementedException();
    }

    public TValue? GetOrCreate<TValue>(string key, Func<TValue> getValueFunc, TimeSpan expireTime)
    {
        throw new NotImplementedException();
    }

    public async Task<TValue> GetOrCreate<TValue>(string key, Func<Task<TValue>> getValueFunc, TimeSpan expireTime)
    {
        throw new NotImplementedException();
    }

    public async Task<TValue> GetAsync<TValue>(string key)
    {
        throw new NotImplementedException();
    }

    public async Task<Task> AddAsync<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        throw new NotImplementedException();
    }
}