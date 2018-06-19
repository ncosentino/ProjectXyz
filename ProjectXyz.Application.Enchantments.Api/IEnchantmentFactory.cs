using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantmentFactory
    {
        IEnchantment Create(IEnumerable<IBehavior> behaviors);
    }
}