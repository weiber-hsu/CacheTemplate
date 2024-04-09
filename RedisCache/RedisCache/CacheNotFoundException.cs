using StackExchange.Redis;

namespace RedisCache;

public class CacheNotFoundException : Exception
{
    public CacheNotFoundException(RedisKey keyWithPostfix)
    {
        throw new NotImplementedException();
    }
}