using ProjectXyz.Api.Framework;
using ProjectXyz.Api.Systems;

namespace ProjectXyz.Plugins.Systems.ElapsedTime
{
    public sealed class ElapsedTime : IElapsedTime
    {
        public ElapsedTime(IInterval interval)
        {
            Interval = interval;
        }

        public IInterval Interval { get; }
    }
}