using RedisCache;

var builder = WebApplication.CreateBuilder(args);
//with k8s port forward
var redisConfig = new RedisConfig("localhost:6379", 1, "_postfix");
var redisCacheService = new RedisCacheService(redisConfig);
// var redis = ConnectionMultiplexer.Connect("localhost:6379");
// var db = redis.GetDatabase();
// db.StringSet("foo", 1688);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();