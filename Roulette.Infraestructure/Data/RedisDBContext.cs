using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Roulette.Core.Interfaces;
using Newtonsoft.Json;
using System.Reflection;

namespace Roulette.Infraestructure.Data
{
    public class RedisDBContext: IRedisDBContext
    {
        private readonly IDatabase _redis;
        public RedisDBContext(IDatabase redis)
        {
            _redis = redis;
        }
        public T Get<T>(string key)
        {
            var value =  _redis.StringGet(key);

            if (value != RedisValue.Null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }
        public List<T> GetAllForKey<T>(string key)
        {
            var value = _redis.StringGet(key);

            if (value != RedisValue.Null)
            {
                return JsonConvert.DeserializeObject<List<T>>(value);
            }

            return default;
        }
        public T Set<T>(string key, T value)
        {
            _redis.StringSet(key, JsonConvert.SerializeObject(value));
            return value;
        }
    }
}
