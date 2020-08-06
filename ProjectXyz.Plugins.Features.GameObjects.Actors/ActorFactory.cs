using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Enchantments.Stats;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.Stats;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Actors.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors
{
    public sealed class ActorFactory : IActorFactory
    {
        private readonly Actor.Factory _actorFactory;
        private readonly IStatManagerFactory _statManagerFactory;
        private readonly IActiveEnchantmentManagerFactory _activeEnchantmentManagerFactory;
        private readonly IBehaviorManager _behaviorManager;
        private readonly IMutableStatsProviderFactory _mutableStatsProviderFactory;

        public ActorFactory(
            Actor.Factory actorFactory,
            IStatManagerFactory statManagerFactory,
            IActiveEnchantmentManagerFactory activeEnchantmentManagerFactory,
            IBehaviorManager behaviorManager,
            IMutableStatsProviderFactory mutableStatsProviderFactory)
        {
            _actorFactory = actorFactory;
            _statManagerFactory = statManagerFactory;
            _activeEnchantmentManagerFactory = activeEnchantmentManagerFactory;
            _behaviorManager = behaviorManager;
            _mutableStatsProviderFactory = mutableStatsProviderFactory;
        }

        public IGameObject Create()
        {
            var identifierBehavior = new IdentifierBehavior();

            var mutableStatsProvider = _mutableStatsProviderFactory.Create();
            var statManager = _statManagerFactory.Create(mutableStatsProvider);
            var hasMutableStats = new HasMutableStatsBehavior(statManager);

            var activeEnchantmentManager = _activeEnchantmentManagerFactory.Create();
            var hasEnchantments = new HasEnchantmentsBehavior(activeEnchantmentManager);
            var buffable = new BuffableBehavior(activeEnchantmentManager);

            var actor = _actorFactory.Invoke(
                identifierBehavior,
                _behaviorManager,
                hasEnchantments,
                buffable,
                hasMutableStats);
            return actor;
        }
    }
}