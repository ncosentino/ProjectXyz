using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Skills.Components;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillDefinition : ISkillDefinition
    {
        public SkillDefinition(
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> filterComponents)
        {
            FilterComponents = filterComponents.ToArray();
            SupportedAttributes = supportedAttributes.ToArray();
        }

        public static ISkillDefinition FromId(string id)
        {
            var idGenerator = new SkillIdentifierGeneratorComponent(id);
            var idFilterAttribute = new FilterAttribute(
                new StringIdentifier("id"),
                new IdentifierFilterAttributeValue(new StringIdentifier(id)),
                false);

            return new SkillDefinition(
                new[] { idFilterAttribute },
                new[] { idGenerator });
        }

        public static ISkillDefinition Anonymous() => FromId(
            Guid.NewGuid().ToString());

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
