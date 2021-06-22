using System.Collections.Generic;

using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapFactory
    {
        IGameObject Create(
            IIdentifier id,
            IEnumerable<IMapLayer> layers,
            IEnumerable<IBehavior> behaviors);
    }
}