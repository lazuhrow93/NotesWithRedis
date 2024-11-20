using StackExchange.Redis;

namespace Note.Data.RedisLibrary;

public interface IRedisKeyProvider
{
    RedisKey MasterKey();
    RedisKey Identifier<T>();
    RedisKey Entity<T>();
}

