using ProjectXyz.Framework.Interface;
using ProjectXyz.Game.Interface.Systems;

namespace ProjectXyz.Game.Core.Systems
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