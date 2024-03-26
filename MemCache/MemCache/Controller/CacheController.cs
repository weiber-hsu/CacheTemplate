using MemCache.Service;
using Microsoft.AspNetCore.Mvc;

//may add white list for security
namespace MemCache.Controller;

[ApiController]
[Route("api")]
public class CacheController : ControllerBase
{
    private ICacheService _memCacheService;

    public CacheController(ICacheService memCacheService)
    {
        _memCacheService = memCacheService;
    }

    [HttpGet("MemGet")]
    public async Task<IActionResult> Get()
    {
        var s = "6666";
        await _memCacheService.AddAsync("key",s, TimeSpan.FromSeconds(10));
        return Ok(await _memCacheService.GetAsync<string>("key"));
    }
}