namespace MemCache.Exception;

public class CacheNotFoundException(string key) : System.Exception($"Key {key} not exist in Cache.");