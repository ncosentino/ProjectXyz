using System.Collections.Generic;
using System.Diagnostics;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    [DebuggerDisplay("Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}")]
    public sealed class Enchantment : IEnchantment
    {
        public Enchantment(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IIdentifier statDefinitionId,
            IEnumerable<IBehavior> behaviors)
        {
            StatDefinitionId = statDefinitionId;
            Behaviors = behaviorCollectionFactory.Create(behaviors);
        }

        public IIdentifier StatDefinitionId { get; }

        public IBehaviorCollection Behaviors { get; }
    }
}