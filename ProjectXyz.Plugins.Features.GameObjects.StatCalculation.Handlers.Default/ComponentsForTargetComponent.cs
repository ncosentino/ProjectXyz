using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class ComponentsForTargetComponent : IComponentsForTargetComponent
    {
        public ComponentsForTargetComponent(
            IGameObject target,
            IEnumerable<IComponent> components)
        {
            Target = target;
            Components = components;
        }

        public IGameObject Target { get; }

        public IEnumerable<IComponent> Components { get; }
    }
}
