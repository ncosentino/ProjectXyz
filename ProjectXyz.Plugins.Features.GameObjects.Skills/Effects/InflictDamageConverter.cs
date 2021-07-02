using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class InflictDamageGeneratorComponent : IGeneratorComponent
    {
    }

    public sealed class InflictDamageConverter : ISkillEffectBehaviorConverter
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
