using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class ActorAnimationOnUseGeneratorComponent : IGeneratorComponent
    {
        public ActorAnimationOnUseGeneratorComponent(IIdentifier animationId)
        {
            AnimationId = animationId;
        }

        public IIdentifier AnimationId { get; }
    }

    public sealed class ActorAnimationOnUseConverter : IBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is ActorAnimationOnUseGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(IGeneratorComponent component)
        {
            var convertedComponent = (ActorAnimationOnUseGeneratorComponent)component;
            var behavior = new ActorAnimationOnUseBehavior(convertedComponent.AnimationId);
            return behavior;
        }
    }
}
