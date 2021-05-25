using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
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
