using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.CommonBehaviors;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Components
{
    public sealed class SkillIdentifierGeneratorComponent : IGeneratorComponent
    {
        public SkillIdentifierGeneratorComponent(string skillDefinitionId)
        {
            SkillDefinitionId = new StringIdentifier(skillDefinitionId);
        }

        public IIdentifier SkillDefinitionId { get; }
    }

    public sealed class SkillIdentifierConverter : IBehaviorConverter
    {
        public bool CanConvert(
            IGeneratorComponent component)
        {
            return component is SkillIdentifierGeneratorComponent;
        }

        public IBehavior ConvertToBehavior(
            IGeneratorComponent component)
        {
            var idComponent = (SkillIdentifierGeneratorComponent)component;

            return new IdentifierBehavior(
                idComponent.SkillDefinitionId);
        }
    }
}
