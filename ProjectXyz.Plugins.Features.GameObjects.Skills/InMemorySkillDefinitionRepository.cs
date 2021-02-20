using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class InMemorySkillDefinitionRepository : IDiscoverableSkillDefinitionRepository
    {
        private readonly IAttributeFilterer _attributeFilterer;
        private readonly IReadOnlyCollection<ISkillDefinition> _skillDefinitions;

        public InMemorySkillDefinitionRepository(
            IAttributeFilterer attributeFilterer,
            IEnumerable<ISkillDefinition> skillDefinitions)
        {
            _attributeFilterer = attributeFilterer;
            _skillDefinitions = skillDefinitions.ToArray();
        }

        public IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext)
        {
            var filtered = _attributeFilterer.Filter(
                _skillDefinitions,
                filterContext);
            return filtered;
        }
    }
}
