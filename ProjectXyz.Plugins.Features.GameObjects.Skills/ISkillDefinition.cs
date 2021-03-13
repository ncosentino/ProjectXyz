using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering.Attributes;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Generation;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinition : IHasFilterAttributes
    {
        IIdentifier SkillDefinitionId { get; }

        IReadOnlyCollection<IIdentifier> SkillSynergyDefinitionIds { get; }

        /// <summary>
        /// Gets the stateful <see cref="IEnchantment"/>s for the skill
        /// definition. 'Stateful' enchantments should include any behaviors 
        /// that have state that can change after their creation that alters 
        /// their behavior. These should *NOT* be copied between enchantment
        /// collections and therefor should be reloaded from the repository
        /// when you wish to apply them to another collection of enchantments.
        /// </summary>
        /// <returns>
        /// An <see cref="IReadOnlyCollection{T}"/> of <see cref="IIdentifier"/>s.
        /// </returns>
        IReadOnlyCollection<IIdentifier> StatefulEnchantmentDefinitions { get; }

        IIdentifier SkillTargetModeId { get; }

        IReadOnlyDictionary<IIdentifier, double> Stats { get; }

        IReadOnlyDictionary<IIdentifier, double> StaticResourceRequirements { get; }

        IEnumerable<IGeneratorComponent> FilterComponents { get; }
    }
}
