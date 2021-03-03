using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers
{
    public interface IComponentsForTargetComponent : IComponent
    {
        IGameObject Target { get; }

        IEnumerable<IComponent> Components { get; }
    }
}
