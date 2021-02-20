using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public sealed class SkillSynergyEffectedSkillsBehavior :
        BaseBehavior,
        ISkillSynergyEffectedSkillsBehavior
    {
        public SkillSynergyEffectedSkillsBehavior(IEnumerable<IIdentifier> skillDefinitionIds)
        {
            SkillDefinitionIds = skillDefinitionIds.ToArray();
        }

        public IReadOnlyCollection<IIdentifier> SkillDefinitionIds { get; }
    }
}
