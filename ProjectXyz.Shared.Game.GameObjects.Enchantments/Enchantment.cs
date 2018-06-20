using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
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

        public override string ToString()
        {
            return $"Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}";
        }
    }
}