using MemCache.Controller;
using MemCache.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddApplicationPart(typeof(CacheController).Assembly);
builder.Services.AddTransient<ICacheService, MemCacheService>();
var app = builder.Build();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", context => context.Response.WriteAsync("Hello World!"));
});
app.Run();
