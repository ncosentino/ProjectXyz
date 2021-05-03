using System.Collections.Generic;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Api
{
    public interface IItemFactory
    {
        IGameObject Create(IEnumerable<IBehavior> behaviors);
    }
}