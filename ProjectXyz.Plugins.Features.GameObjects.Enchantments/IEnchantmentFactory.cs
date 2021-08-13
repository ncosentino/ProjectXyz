using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Api.Enchantments
{
    public interface IEnchantmentFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}