using System;
using ClassLibrary1.Framework.Interface;

namespace ClassLibrary1.Framework.Shared
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