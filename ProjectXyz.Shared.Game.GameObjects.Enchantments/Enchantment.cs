using System.Collections.Generic;
using System.Diagnostics;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    [DebuggerDisplay("Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}")]
    public sealed class Enchantment : IEnchantment
    {
        public Enchantment(
            IBehaviorCollectionFactory behaviorCollectionFactory,
            IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviorCollectionFactory.Create(behaviors);
        }

        public IBehaviorCollection Behaviors { get; }
    }
}