using System;
using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Framework.Shared
{
    public sealed class TryInReleaseMode : ITry
    {
        public Exception Dangerous(Action callback)
        {

#if !DEBUG
            try
            {
#endif
            callback();
#if !DEBUG
            }
            catch (Exception ex)
            {
                return ex;
            }
#endif

            return null;
        }
    }
}