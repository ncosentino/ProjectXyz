using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.Default
{
    internal sealed class Enchantment : IEnchantment
    {
        public Enchantment(IEnumerable<IBehavior> behaviors)
        {
            Behaviors = behaviors.ToArray();
        }

        public IReadOnlyCollection<IBehavior> Behaviors { get; }
    }
}