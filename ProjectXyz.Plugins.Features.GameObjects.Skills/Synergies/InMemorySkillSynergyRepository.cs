using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class InMemorySkillSynergyRepository : IDiscoverableSkillSynergyRepository
    {
        public IEnumerable<IGameObject> GetSkillSynergies(IFilterContext filterContext)
        {
            yield break;
        }
    }
}
