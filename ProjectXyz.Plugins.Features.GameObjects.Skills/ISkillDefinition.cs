using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinition : IHasFilterAttributes
    {
        IIdentifier SkillDefinitionId { get; }

        IReadOnlyCollection<IIdentifier> SkillSynergyDefinitionIds { get; }

        IIdentifier SkillTargetModeId { get; }

        IReadOnlyDictionary<IIdentifier, double> Stats { get; }

        IEnumerable<IFilterComponent> FilterComponents { get; }
    }
}
