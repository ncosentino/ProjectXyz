using System;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Framework.Shared
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