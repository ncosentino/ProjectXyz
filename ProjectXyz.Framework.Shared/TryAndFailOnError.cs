using System;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
{
    public sealed class TryAndFailOnError : ITry
    {
        public Exception Dangerous(Action callback)
        {
            callback();
            return null;
        }
    }
}