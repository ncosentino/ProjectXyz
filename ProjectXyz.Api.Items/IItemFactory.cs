using System.Collections.Generic;
using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api
{
    public interface IItemFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}