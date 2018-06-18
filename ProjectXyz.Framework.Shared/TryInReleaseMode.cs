using System;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Framework
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