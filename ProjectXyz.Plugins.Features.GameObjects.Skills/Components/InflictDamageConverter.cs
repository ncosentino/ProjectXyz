using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class InflictDamageGeneratorComponent : IGeneratorComponent
    {
    }

    public sealed class InflictDamageConverter : IBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is InflictDamageGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            return new InflictDamageBehavior();
        }
    }
}
