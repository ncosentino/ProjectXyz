using System;

namespace ProjectXyz.Plugins.Features.ElapsedTime.Default
{
    public sealed class RealTimeProvider : IRealTimeProvider
    {
        public DateTime GetTimeUtc() => DateTime.UtcNow;
    }
}
