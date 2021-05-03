using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasReadOnlyEnchantmentsBehavior : IBehavior
    {
        IReadOnlyCollection<IGameObject> Enchantments { get; }
    }
}