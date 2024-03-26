namespace MemCache.Service;

public interface ICacheService
{
    void Add<TValue>(string key, TValue value, TimeSpan expireTime);
    TValue Get<TValue>(string key);
    void Delete(string key);
    bool TryGet<TValue>(string key, out TValue? value);
    TValue? GetOrCreate<TValue>(string key, Func<TValue> getValueFunc, TimeSpan expireTime);
    Task<TValue> GetOrCreate<TValue>(string key, Func<Task<TValue>> getValueFunc, TimeSpan expireTime);
    Task<TValue> GetAsync<TValue>(string key);
    Task<Task> AddAsync<TValue>(string key, TValue value, TimeSpan expireTime);
}