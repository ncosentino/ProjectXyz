﻿using System.Collections.Generic;

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
