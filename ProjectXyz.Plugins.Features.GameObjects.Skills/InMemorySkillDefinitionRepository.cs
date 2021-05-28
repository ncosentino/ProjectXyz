using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

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

        /// <inheritdoc/>
        public IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext)
        {
            var filtered = _attributeFilterer.BidirectionalFilter(
                _skillDefinitions,
                filterContext.Attributes);
            return filtered;
        }
    }
}
