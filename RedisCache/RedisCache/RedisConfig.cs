using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace RedisCache;

public class RedisConfig
{
    private readonly ConnectionMultiplexer _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionString">Redis Connection String.</param>
    /// <param name="dbNumber">Redis database number, can be 0 to 15</param>
    /// <param name="keyPostfix">Postfix to prevent key duplicated with other project. For example if set this value to '_cc' for cache key 'my_cache_key', the real cache key will be 'my_cache_key_cc'</param>
    public RedisConfig(string connectionString, int dbNumber = 1, string keyPostfix = "")
    {
        KeyPostfix = keyPostfix;
        _connection = ConnectionMultiplexer.Connect($"{connectionString},defaultDatabase={dbNumber}");
    }

    private RedisConfig(ConfigurationOptions options, string keyPostfix = "")
    {
        KeyPostfix = keyPostfix;
        _connection = ConnectionMultiplexer.Connect(options);
    }

    public string KeyPostfix { get; }

    public IDatabase GetConnection()
    {
        return _connection.GetDatabase();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbNumber">Redis database number, can be 0 to 15</param>
    /// <param name="keyPostfix">Postfix to prevent key duplicated with other project. For example if set this value to '_cc' for cache key 'my_cache_key', the real cache key will be 'my_cache_key_cc'</param>
    /// <returns></returns>
    public static RedisConfig UseSealCluster(string password, int dbNumber = 1, string keyPostfix = "")
    {
        var redisConfiguration = new RedisConfiguration()
        {
            Hosts = new RedisHost[]
            {
                new()
                {
                    Host = "localhost",
                    Port = 6379
                },
            },
            AbortOnConnectFail = false,
            Database = dbNumber,
            ConnectTimeout = 5000,
            Password = password,
            SyncTimeout = 5000
        };

        return new RedisConfig(redisConfiguration.ConfigurationOptions, keyPostfix);
    }

}