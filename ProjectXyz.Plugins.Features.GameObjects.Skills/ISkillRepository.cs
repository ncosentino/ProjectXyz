using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillRepository
    {
        IEnumerable<IGameObject> GetSkills(IFilterContext filterContext);
    }
}
