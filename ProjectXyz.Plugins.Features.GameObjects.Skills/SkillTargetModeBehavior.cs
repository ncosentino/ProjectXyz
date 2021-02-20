using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillTargetModeBehavior :
        BaseBehavior,
        ISkillTargetModeBehavior
    {
        public SkillTargetModeBehavior(IIdentifier targetModeId)
        {
            TargetModeId = targetModeId;
        }

        public IIdentifier TargetModeId { get; }
    }
}
