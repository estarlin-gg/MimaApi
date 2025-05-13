using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mima.Application.Cache
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
        void Remove(string key);
        bool Exists(string key);
    }
}
