using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;

namespace ProjectXyz.Api.GameObjects.Items
{
    public interface IItemFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}