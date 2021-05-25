using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergyRepositoryFacade : ISkillSynergyRepositoryFacade
    {
        private readonly IReadOnlyCollection<ISkillSynergyRepository> _repositories;

        public SkillSynergyRepositoryFacade(IEnumerable<IDiscoverableSkillSynergyRepository> repositories)
        {
            _repositories = repositories.ToArray();
        }

        // FIXME: if there are multiple repositories, this needs to ensure it 
        // can respect the counts that are defined on the context!!
        public IEnumerable<IGameObject> GetSkillSynergies(IFilterContext filterContext) =>
            _repositories.SelectMany(x => x.GetSkillSynergies(filterContext));
    }
}
