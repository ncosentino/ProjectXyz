using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyEffectedSkillsBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> SkillDefinitionIds { get; }
    }
}
