using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinitionRepository
    {
        IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext);
    }
}
