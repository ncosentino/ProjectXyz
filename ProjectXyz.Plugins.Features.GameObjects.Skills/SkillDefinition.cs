using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Shared.Behaviors.Filtering.Attributes;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public sealed class SkillDefinition : ISkillDefinition
    {
        public SkillDefinition(
            IIdentifier skillDefinitionId,
            IIdentifier skillTargetModeId,
            IReadOnlyCollection<IIdentifier> skillSynergyDefinitionIds,
            IEnumerable<KeyValuePair<IIdentifier, double>> stats,
            IEnumerable<IFilterAttribute> supportedAttributes)
        {
            SkillDefinitionId = skillDefinitionId;
            SkillTargetModeId = skillTargetModeId;
            SkillSynergyDefinitionIds = skillSynergyDefinitionIds;
            SupportedAttributes =
                new FilterAttribute(
                    new StringIdentifier("id"),
                    new IdentifierFilterAttributeValue(skillDefinitionId),
                    false)
                .Yield()
                .Concat(supportedAttributes)
                .ToArray();
            Stats = stats.ToDictionary();
        }

        public IIdentifier SkillDefinitionId { get; }

        public IIdentifier SkillTargetModeId { get; }

        public IReadOnlyCollection<IIdentifier> SkillSynergyDefinitionIds { get; }

        public IReadOnlyDictionary<IIdentifier, double> Stats { get; }

        public IEnumerable<IFilterAttribute> SupportedAttributes { get; }
    }
}
