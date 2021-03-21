using System;

namespace ProjectXyz.Plugins.Features.ElapsedTime
{
    public sealed class RealTimeProvider : IRealTimeProvider
    {
        public DateTime GetTimeUtc() => DateTime.UtcNow;
    }
}
