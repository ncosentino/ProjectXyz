using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Enchantments;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IEnchantableBehavior : IBehavior
    {
        void Enchant(IEnumerable<IEnchantment> enchantments);
    }
}
