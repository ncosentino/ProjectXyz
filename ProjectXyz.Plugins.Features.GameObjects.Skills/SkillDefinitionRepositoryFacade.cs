using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillDefinitionRepositoryFacade : ISkillDefinitionRepositoryFacade
    {
        private readonly IReadOnlyCollection<ISkillDefinitionRepository> _repositories;

        public SkillDefinitionRepositoryFacade(IEnumerable<IDiscoverableSkillDefinitionRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        // FIXME: if there are multiple repositories, this needs to ensure it 
        // can respect the counts that are defined on the context!!
        public IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext) =>
            _repositories.SelectMany(x => x.GetSkillDefinitions(filterContext));
    }
}
