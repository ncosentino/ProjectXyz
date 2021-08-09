using ProjectXyz.Plugins.Features.Stats;

namespace ProjectXyz.Api.Enchantments.Stats
{
    public interface IStatManagerFactory
    {
        IStatManager Create(IMutableStatsProvider mutableStatsProvider);
    }
}