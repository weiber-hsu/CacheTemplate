using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

//may add white list for security
namespace MemCache.Controller;

[ApiController]
[Route("api")]
public class CacheController : ControllerBase
{
    [HttpGet("register")]
    public async Task<IActionResult> Get()
    {
        var memCacheService = new MemCacheService();
        return Ok("123");
    }
}

public class MemCacheService : IMemoryCache
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public bool TryGetValue(object key, out object? value)
    {
        throw new NotImplementedException();
    }

    public ICacheEntry CreateEntry(object key)
    {
        throw new NotImplementedException();
    }

    public void Remove(object key)
    {
        throw new NotImplementedException();
    }
}