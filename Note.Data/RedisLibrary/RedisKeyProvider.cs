using StackExchange.Redis;

namespace Note.Data.RedisLibrary;

public class RedisKeyProvider : IRedisKeyProvider
{
    private const string _delimiter = ":";

    public RedisKey MasterKey()
    {
        return new RedisKey($"identifier");
    }

    public RedisKey Identifier<T>()
    {
        return new RedisKey($"identifier{_delimiter}{typeof(T).Name}");
    }

    public RedisKey Entity<T>()
    {
        return new RedisKey($"{typeof(T).Name}");
    }
}

