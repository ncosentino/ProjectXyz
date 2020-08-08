using System;

namespace ProjectXyz.Api.Framework
{
    public interface ICast
    {
        object ToType(
            object obj,
            Type resultType);

        object ToType(
            object obj,
            Type resultType,
            bool useCache);
    }
}
