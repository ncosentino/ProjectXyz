using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;

using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills.Effects
{
    public sealed class SkillEffectDefinition : ISkillEffectDefinition
    {
        public SkillEffectDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> filterComponents)
        {
            FilterComponents = filterComponents.ToArray();
            SupportedAttributes = supportedAttributes.ToArray();
        }

        public static ISkillEffectDefinition New => new SkillEffectDefinition(
            Enumerable.Empty<IFilterAttribute>(),
            Enumerable.Empty<IGeneratorComponent>());

        public IEnumerable<IGeneratorComponent> FilterComponents { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
