using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Plugins.Features.Mapping;

namespace ProjectXyz.Plugins.Features.Mapping.Default
{
    public sealed class MapBehaviorsProviderFacade : IMapBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableMapBehaviorsProvider> _providers;

        public MapBehaviorsProviderFacade(IEnumerable<IDiscoverableMapBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}