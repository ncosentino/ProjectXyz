using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillRepository : ISkillRepository
    {
        private readonly IBehaviorConverterFacade _behaviorConverter;
        private readonly ISkillDefinitionRepositoryFacade _skillDefinitionRepositoryFacade;
        private readonly IGameObjectFactory _gameObjectFactory;

        public SkillRepository(
            IGameObjectFactory gameObjectFactory,
            ISkillDefinitionRepositoryFacade skillDefinitionRepositoryFacade,
            IBehaviorConverterFacade behaviorConverter)
        {
            _gameObjectFactory = gameObjectFactory;
            _skillDefinitionRepositoryFacade = skillDefinitionRepositoryFacade;
            _behaviorConverter = behaviorConverter;
        }

        public IEnumerable<IGameObject> GetSkills(IFilterContext filterContext)
        {
            foreach (var skillDefinition in _skillDefinitionRepositoryFacade.GetSkillDefinitions(filterContext))
            {
                // TODO: Synergies, Prereqs?, Reqs?
                var generatorComponentBehaviors = skillDefinition
                    .FilterComponents
                    .Where(x => _behaviorConverter.CanConvert(x))
                    .Select(x => _behaviorConverter.ConvertToBehavior(x))
                    .ToArray();

                var skill = _gameObjectFactory.Create(
                    generatorComponentBehaviors);

                yield return skill;
            }
        }
    }
}
