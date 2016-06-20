using System;

namespace ProjectXyz.Framework.Interface
{
    public interface ITry
    {
        Exception Dangerous(Action callback);
    }
}