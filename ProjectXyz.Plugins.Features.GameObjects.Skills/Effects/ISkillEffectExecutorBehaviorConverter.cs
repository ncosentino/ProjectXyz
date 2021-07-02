using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public interface ISkillEffectExecutorBehaviorConverter
    {
        bool CanConvert(IGeneratorComponent component);

        IGameObject ConvertToExecutor(IGeneratorComponent component);
    }
}
