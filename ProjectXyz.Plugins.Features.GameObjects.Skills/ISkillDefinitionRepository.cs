using System.Collections.Generic;

using ProjectXyz.Api.Behaviors.Filtering;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface ISkillDefinitionRepository
    {
        IEnumerable<ISkillDefinition> GetSkillDefinitions(IFilterContext filterContext);
        /// <summary>
        /// Gets the stateful <see cref="IEnchantment"/>s for a given skill 
        /// definition <see cref="IIdentifier"/>. 'Stateful' enchantments 
        /// should include any behaviors that have state that can change after 
        /// their creation that alters their behavior. These should *NOT* be 
        /// copied between enchantment collections and therefor should be
        /// reloaded from the repository when you wish to apply them to another
        /// collection of enchantments.
        /// </summary>
        /// <param name="skillDefinitionId">The ID of the skill definition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IEnchantment"/>s.</returns>
        IEnumerable<IEnchantment> GetSkillDefinitionStatefulEnchantments(IIdentifier skillDefinitionId);
    }
}
