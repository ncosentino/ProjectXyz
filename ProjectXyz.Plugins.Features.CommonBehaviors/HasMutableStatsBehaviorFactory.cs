using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasMutableStatsBehaviorFactory : IHasMutableStatsBehaviorFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public HasMutableStatsBehaviorFactory(
            IStatManagerFactory statManagerFactory,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _statManagerFactory = statManagerFactory;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IHasMutableStatsBehavior Create()
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var behavior = Create(statManager);
            return behavior;
        }

        public IHasMutableStatsBehavior Create(IStatManager statManager)
        {
            var behavior = new HasMutableStatsBehavior(statManager);
            return behavior;
        }
    }
}