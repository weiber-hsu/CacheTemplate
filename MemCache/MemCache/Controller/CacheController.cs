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
        return Ok("123");
    }
}