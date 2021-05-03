using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillTargetModeBehavior : IBehavior
    {
        IIdentifier TargetModeId { get; }
    }
}
