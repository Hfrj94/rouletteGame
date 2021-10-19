using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roulette.Core.Interfaces
{
    public interface IRedisDBContext
    {
        T Get<T>(string key);
        public List<T> GetAllForKey<T>(string key);
        T Set<T>(string key, T value);
    }
}
