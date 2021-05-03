using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Synergies
{
    public interface ISkillSynergyDefinitionIdBehavior : IBehavior
    {
        IIdentifier SkillSynergyDefinitionId { get; }
    }
}
