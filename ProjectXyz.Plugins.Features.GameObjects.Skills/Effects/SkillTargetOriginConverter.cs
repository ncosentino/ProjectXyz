using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillTargetOriginGeneratorComponent : IGeneratorComponent
    {
        public SkillTargetOriginGeneratorComponent(
            int offsetFromCasterX,
            int offsetFromCasterY)
        {
            OffsetFromCasterX = offsetFromCasterX;
            OffsetFromCasterY = offsetFromCasterY;
        }

        public int OffsetFromCasterX { get; }

        public int OffsetFromCasterY { get; }
    }

    public sealed class SkillTargetOriginConverter : ISkillEffectBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is SkillTargetOriginGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var targetOriginComponent = (SkillTargetOriginGeneratorComponent)component;

            return new TargetOriginBehavior(
                targetOriginComponent.OffsetFromCasterX,
                targetOriginComponent.OffsetFromCasterY);
        }
    }
}
