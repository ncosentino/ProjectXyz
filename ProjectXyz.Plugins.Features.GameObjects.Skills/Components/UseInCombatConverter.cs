using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class UseInCombatGeneratorComponent : IGeneratorComponent
    {
    }

    public sealed class UseInCombatConverter : IBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is UseInCombatGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            return new UseInCombatBehavior();
        }
    }
}
