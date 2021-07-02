using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectBehaviorConverter
    {
        bool CanConvert(IGeneratorComponent component);

        IBehavior ConvertToBehavior(IGeneratorComponent component);
    }
}
