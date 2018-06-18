using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Api.Items
{
    public interface IItemFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}