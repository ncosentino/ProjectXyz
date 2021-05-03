using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillBehaviorsProviderFacade : ISkillBehaviorsProviderFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableSkillBehaviorsProvider> _providers;

        public SkillBehaviorsProviderFacade(IEnumerable<IDiscoverableSkillBehaviorsProvider> providers)
        {
            _providers = providers.ToArray();
        }

        public IEnumerable<IBehavior> GetBehaviors(IReadOnlyCollection<IBehavior> baseBehaviors) =>
            _providers.SelectMany(x => x.GetBehaviors(baseBehaviors));
    }
}
