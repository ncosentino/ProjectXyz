using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class DisplayIconGeneratorComponent : IGeneratorComponent
    {
        public DisplayIconGeneratorComponent(string resourcePath)
        {
            IconResourceId = new StringIdentifier(resourcePath);
        }

        public IIdentifier IconResourceId { get; }
    }

    public sealed class DisplayIconConverter : IBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is DisplayIconGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var displayIconComponent = (DisplayIconGeneratorComponent)component;

            return new HasDisplayIconBehavior(
                displayIconComponent.IconResourceId);
        }
    }
}
