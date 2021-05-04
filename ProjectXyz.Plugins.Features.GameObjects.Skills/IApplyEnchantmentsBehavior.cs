using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Plugins.Features.GameObjects.Skills
{
    public interface IApplyEnchantmentsBehavior : IBehavior
    {
        IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }
    }

    public sealed class ApplyEnchantmentsBehavior : BaseBehavior, IApplyEnchantmentsBehavior
    {
        public ApplyEnchantmentsBehavior(
            IEnumerable<IIdentifier> enchantmentDefinitionIds)
        {
            EnchantmentDefinitionIds = enchantmentDefinitionIds
                .ToArray();
        }

        public IReadOnlyCollection<IIdentifier> EnchantmentDefinitionIds { get; }
    }
}
