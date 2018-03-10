using ProjectXyz.Api.Framework;

namespace ProjectXyz.Api.Systems
{
    public interface IElapsedTime
    {
        IInterval Interval { get; }
    }
}