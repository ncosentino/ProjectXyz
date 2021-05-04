using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class DisplayNameComponentGenerator : IGeneratorComponent
    {
        public DisplayNameComponentGenerator(string displayName)
        {
            DisplayName = displayName;
        }

        public string DisplayName { get; }
    }

    public sealed class DisplayNameConverter : IBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is DisplayNameComponentGenerator;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var displayNameComponent = (DisplayNameComponentGenerator)component;

            return new HasDisplayNameBehavior(
                displayNameComponent.DisplayName);
        }
    }
}
