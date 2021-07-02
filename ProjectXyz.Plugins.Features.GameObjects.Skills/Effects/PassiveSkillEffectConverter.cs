using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class PassiveSkillEffectGeneratorComponent : IGeneratorComponent
    {
    }

    public sealed class PassiveSkillEffectConverter : ISkillEffectBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is PassiveSkillEffectGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(IGeneratorComponent component)
        {
            var behavior = new PassiveSkillEffectBehavior();
            return behavior;
        }
    }
}
