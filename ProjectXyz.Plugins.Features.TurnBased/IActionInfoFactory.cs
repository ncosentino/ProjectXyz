using NexusLabs.Collections.Generic;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.TurnBased
{
    public interface IActionInfoFactory
    {
        IActionInfo Create(
            IGameObject actor,
            IFrozenCollection<IGameObject> applicableGameObjects,
            double actions);
    }
}
