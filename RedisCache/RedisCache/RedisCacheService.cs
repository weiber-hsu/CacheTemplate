using System.Text.Json;
using StackExchange.Redis;

namespace RedisCache;

public class RedisCacheService(RedisConfig redisConfig) : ICacheService
{
    private readonly string _keyPostfix = redisConfig.KeyPostfix;
    private readonly IDatabase _redisDb = redisConfig.GetConnection();

    public void Add<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        _redisDb.StringSet(CreateKeyWithPostfix(key), JsonSerializer.Serialize(value), expireTime);
    }

    public TValue Get<TValue>(string key)
    {
        var keyWithPostfix = CreateKeyWithPostfix(key);
        var isExist = TryGetWithoutPostfix<TValue>(keyWithPostfix, out var result);

        if (!isExist)
        {
            throw new CacheNotFoundException(keyWithPostfix);
        }

        return result;
    }

    public void Delete(string key)
    {
        _redisDb.KeyDelete(CreateKeyWithPostfix(key));
    }

    public bool TryGet<TValue>(string key, out TValue? value)
    {
        var keyWithPostfix = CreateKeyWithPostfix(key);

        var isExist = TryGetWithoutPostfix(keyWithPostfix, out TValue result);
        value = result;
        return isExist;
    }

    public TValue? GetOrCreate<TValue>(string key, Func<TValue> getValueFunc, TimeSpan expireTime)
    {
        var keyWithPostfix = CreateKeyWithPostfix(key);

        if (TryGetWithoutPostfix(keyWithPostfix, out TValue cacheValue))
        {
            return cacheValue;
        }

        var value = getValueFunc();
        _redisDb.StringSet(keyWithPostfix, JsonSerializer.Serialize(value), expireTime);
        return value;
        ;
    }

    public async Task<TValue> GetOrCreate<TValue>(string key, Func<Task<TValue>> getValueFunc, TimeSpan expireTime)
    {
        var keyWithPostfix = CreateKeyWithPostfix(key);
        var stringGetAsync = await _redisDb.StringGetAsync(keyWithPostfix);

        if (stringGetAsync.HasValue)
        {
            return JsonSerializer.Deserialize<TValue>(stringGetAsync.ToString());
        }

        var value = await getValueFunc();
        await _redisDb.StringSetAsync(keyWithPostfix, JsonSerializer.Serialize(value), expireTime);
        return value;
    }

    public async Task<TValue> GetAsync<TValue>(string key)
    {
        var keyWithPostfix = CreateKeyWithPostfix(key);

        var result = await _redisDb.StringGetAsync(keyWithPostfix);

        if (!result.HasValue)
        {
            throw new CacheNotFoundException(keyWithPostfix);
        }

        return JsonSerializer.Deserialize<TValue>(result);
    }

    public async Task<Task> AddAsync<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        throw new NotImplementedException();
    }

    private RedisKey CreateKeyWithPostfix(string key)
    {
        return key + _keyPostfix;
        ;
    }

    private bool TryGetWithoutPostfix<TValue>(string key, out TValue value)
    {
        try
        {
            var redisValue = _redisDb.StringGet(key);

            if (!redisValue.HasValue)
            {
                value = default;
                return false;
            }

            var cacheValue = JsonSerializer.Deserialize<TValue>(redisValue.ToString());
            value = cacheValue;
            return cacheValue != null;
        }
        catch (Exception e)
        {
            value = default;
            return false;
        }
    }
}