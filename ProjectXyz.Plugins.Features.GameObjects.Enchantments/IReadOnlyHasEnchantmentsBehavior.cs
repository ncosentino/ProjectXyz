using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments
{
    public interface IReadOnlyHasEnchantmentsBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }
    }
}