using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ITargetOriginBehavior : IBehavior
    {
        int OffsetFromCasterX { get; }

        int OffsetFromCasterY { get; }
    }

    public sealed class TargetOriginBehavior : BaseBehavior, ITargetOriginBehavior
    {
        public TargetOriginBehavior(
            int offsetFromCasterX,
            int offsetFromCasterY)
        {
            OffsetFromCasterX = offsetFromCasterX;
            OffsetFromCasterY = offsetFromCasterY;
        }

        public int OffsetFromCasterX { get; }

        public int OffsetFromCasterY { get; }
    }
}
