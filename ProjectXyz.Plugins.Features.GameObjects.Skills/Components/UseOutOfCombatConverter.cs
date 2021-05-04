using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class UseOutOfCombatGeneratorComponent : IGeneratorComponent
    {
    }

    public sealed class UseOutOfCombatConverter : IBehaviorConverter
    {
        public bool CanConvert(IGeneratorComponent component)
        {
            return component is UseOutOfCombatGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            return new UseOutOfCombatBehavior();
        }
    }
}
