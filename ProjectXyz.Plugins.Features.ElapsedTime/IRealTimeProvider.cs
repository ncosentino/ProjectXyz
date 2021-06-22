using System;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public interface IRealTimeProvider
    {
        DateTime GetTimeUtc();
    }
}
