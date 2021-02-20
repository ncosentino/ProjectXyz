using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class HasSkillSynergiesBehavior :
        BaseBehavior,
        IHasSkillSynergiesBehavior
    {
        public HasSkillSynergiesBehavior(IEnumerable<IGameObject> skillSynergies)
        {
            SkillSynergies = skillSynergies.ToArray();
        }

        public IReadOnlyCollection<IGameObject> SkillSynergies { get; }
    }
}
