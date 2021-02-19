using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Shared.Game.GameObjects.Enchantments
{
    [DebuggerDisplay("Expression Enchantment\r\n\tStat Definition Id={StatDefinitionId}")]
    public sealed class Enchantment : IEnchantment
    {
        public Enchantment(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}