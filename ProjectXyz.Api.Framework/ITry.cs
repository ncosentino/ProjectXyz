using System;

namespace ProjectXyz.Api.Framework
{
    public interface ITry
    {
        Exception Dangerous(Action callback);
    }
}