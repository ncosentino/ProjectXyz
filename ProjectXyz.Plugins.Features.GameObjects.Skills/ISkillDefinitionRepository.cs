using System.Collections.Generic;

using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinitionRepository
    {
        IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext);
    }
}
