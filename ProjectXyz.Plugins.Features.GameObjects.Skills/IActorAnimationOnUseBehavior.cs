using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IActorAnimationOnUseBehavior : IBehavior
    {
        IIdentifier AnimationId { get; }
    }

    public sealed class ActorAnimationOnUseBehavior : 
        BaseBehavior,
        IActorAnimationOnUseBehavior
    {
        public ActorAnimationOnUseBehavior(IIdentifier animationId)
        {
            AnimationId = animationId;
        }

        public IIdentifier AnimationId { get; }
    }
}
