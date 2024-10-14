using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Note.Data
{
    public static class RedisHelpers
    {
        public static string ToRedisValue(this object? value)
        {
            if(value == null)
                return string.Empty;

            return JsonSerializer.Serialize(value);
        }
    }
}
