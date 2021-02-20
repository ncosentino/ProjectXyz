using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyRepository
    {
        IEnumerable<IGameObject> GetSkillSynergies(IFilterContext filterContext);
    }
}
