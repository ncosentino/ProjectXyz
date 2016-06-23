using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Framework.Shared
{
    public static class KeyValuePair
    {
        public static KeyValuePair<TKey, TValue> Create<TKey, TValue>(
            TKey key,
            TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }
    }
}
