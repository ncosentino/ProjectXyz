using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Plugins.Features.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
{
    public sealed class HasStatsBehaviorFactory : IHasStatsBehaviorFactory
    {
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public HasStatsBehaviorFactory(
            IStatManagerFactory statManagerFactory,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _statManagerFactory = statManagerFactory;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IHasStatsBehavior Create()
        {
            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var behavior = Create(statManager);
            return behavior;
        }

        public IHasStatsBehavior Create(IStatManager statManager)
        {
            var behavior = new HasStatsBehavior(statManager);
            return behavior;
        }
    }
}