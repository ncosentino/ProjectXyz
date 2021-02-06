using System;
using System.Collections.Generic;
using System.Linq;

using NexusLabs.Framework;

namespace ProjectXyz.Api.Framework.Collections
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(
            this IEnumerable<T> source,
            IRandom random)
        {
            var current = default(T);
            var count = 0;
            foreach (var element in source)
            {
                count++;

                if (random.Next(0, count) == 0)
                {
                    current = element;
                }
            }

            if (count == 0)
            {
                throw new InvalidOperationException("Sequence was empty");
            }

            return current;
        }

        public static T RandomOrDefault<T>(
            this IEnumerable<T> source,
            IRandom random)
        {
            var current = default(T);
            var count = 0;
            foreach (var element in source)
            {
                count++;

                if (random.Next(0, count) == 0)
                {
                    current = element;
                }
            }

            if (count == 0)
            {
                return default(T);
            }

            return current;
        }
    }
}