using MemCache.Exception;
using Microsoft.Extensions.Caching.Memory;

namespace MemCache.Service;

public class MemCacheService : ICacheService
{
    private readonly IMemoryCache _memCache;

    public MemCacheService(IMemoryCache memCache)
    {
        _memCache = memCache;
    }

    public void Add<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        _memCache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = expireTime;
            return value;
        });;
    }

    public TValue Get<TValue>(string key)
    {
        var exist = TryGet<TValue>(key, out var value);

        if (!exist)
        {
            throw new CacheNotFoundException(key);
        }

        return value;;
    }

    public void Delete(string key)
    {
        _memCache.Remove(key);
    }

    public bool TryGet<TValue>(string key, out TValue? value)
    {
        return _memCache.TryGetValue(key, out value);
    }

    public TValue? GetOrCreate<TValue>(string key, Func<TValue> getValueFunc, TimeSpan expireTime)
    {
        return _memCache.GetOrCreate(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = expireTime;
            return getValueFunc();
        });
    }

    public async Task<TValue> GetOrCreate<TValue>(string key, Func<Task<TValue>> getValueFunc, TimeSpan expireTime)
    {
        return await _memCache.GetOrCreateAsync(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = expireTime;
            return getValueFunc();
        });
    }

    public async Task<TValue> GetAsync<TValue>(string key)
    {
        var exist = TryGet<TValue>(key, out var value);

        if (!exist)
        {
            throw new CacheNotFoundException(key);
        }

        return await Task.FromResult(value);
    }

    public async Task<Task> AddAsync<TValue>(string key, TValue value, TimeSpan expireTime)
    {
        Add(key, value, expireTime);
        return Task.CompletedTask;
    }
}