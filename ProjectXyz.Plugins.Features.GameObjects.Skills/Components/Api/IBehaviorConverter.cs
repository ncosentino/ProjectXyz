using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api
{
    public interface IBehaviorConverter
    {
        bool CanConvert(IGeneratorComponent component);

        IBehavior ConvertToBehavior(IGeneratorComponent component);
    }

    public interface IExecutorBehaviorConverter
    {
        bool CanConvert(IGeneratorComponent component);

        ISkillExecutorBehavior ConvertToBehavior(IGeneratorComponent component);
    }
}
