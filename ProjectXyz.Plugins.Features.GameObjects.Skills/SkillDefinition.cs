using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;
using ProjectXyz.Plugins.Features.Behaviors.Filtering.Default.Attributes; // FIXME: dependency on non-API

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillDefinition : ISkillDefinition
    {
        public SkillDefinition(
            IIdentifier skillDefinitionId,
            IIdentifier skillTargetModeId,
            IEnumerable<IIdentifier> skillSynergyDefinitionIds,
            IEnumerable<IIdentifier> statefulEnchantmentDefinitions,
            IEnumerable<KeyValuePair<IIdentifier, double>> stats,
            IEnumerable<IFilterAttribute> supportedAttributes,
            IEnumerable<IGeneratorComponent> filterComponents,
            IEnumerable<KeyValuePair<IIdentifier, double>> staticResourceRequirements,
            ISkillIdentifiers skillIdentifiers)
        {
            SkillDefinitionId = skillDefinitionId;
            SkillTargetModeId = skillTargetModeId;
            SkillSynergyDefinitionIds = skillSynergyDefinitionIds.ToArray();
            StatefulEnchantmentDefinitions = statefulEnchantmentDefinitions.ToArray();
            SupportedAttributes =
                new FilterAttribute(
                    skillIdentifiers.SkillDefinitionIdentifier,
                    new IdentifierFilterAttributeValue(skillDefinitionId),
                    false)
                .Yield()
                .Concat(supportedAttributes)
                .ToArray();
            FilterComponents = filterComponents;
            StaticResourceRequirements = staticResourceRequirements.ToDictionary();
            Stats = stats.ToDictionary();
        }

        public IIdentifier SkillDefinitionId { get; }

        public IIdentifier SkillTargetModeId { get; }

        public IReadOnlyCollection<IIdentifier> SkillSynergyDefinitionIds { get; }

        public IReadOnlyCollection<IIdentifier> StatefulEnchantmentDefinitions { get; }

        public IReadOnlyDictionary<IIdentifier, double> Stats { get; }

        public IReadOnlyDictionary<IIdentifier, double> StaticResourceRequirements { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }

        public IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
