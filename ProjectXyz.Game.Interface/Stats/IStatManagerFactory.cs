using ProjectXyz.Application.Stats.Interface;

namespace ProjectXyz.Game.Interface.Stats
{
    public interface IStatManagerFactory
    {
        IStatManager Create(IMutableStatsProvider mutableStatsProvider);
    }
}