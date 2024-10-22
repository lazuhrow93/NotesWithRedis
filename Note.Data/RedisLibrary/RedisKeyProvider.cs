using StackExchange.Redis;

namespace Note.Data.RedisLibrary;

public interface IRedisKeyProvider
{
    RedisKey Identifier<T>();
    RedisKey Model<T>();
}

public class RedisKeyProvider : IRedisKeyProvider
{
    private const string _delimiter = ":";

    public RedisKey Identifier<T>()
    {
        return new RedisKey($"identifier{_delimiter}{nameof(T)}");
    }

    public RedisKey Model<T>()
    {
        return new RedisKey($"{nameof(T)}");
    }
}

