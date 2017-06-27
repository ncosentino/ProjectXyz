using ProjectXyz.Framework.Interface;

namespace ProjectXyz.Game.Interface.Systems
{
    public interface IElapsedTime
    {
        IInterval Interval { get; }
    }
}