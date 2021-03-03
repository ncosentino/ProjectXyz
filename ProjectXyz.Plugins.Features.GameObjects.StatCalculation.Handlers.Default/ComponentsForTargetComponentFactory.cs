using System.Collections.Generic;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class ComponentsForTargetComponentFactory : IComponentsForTargetComponentFactory
    {
        public IComponentsForTargetComponent Create(
            IGameObject target,
            IEnumerable<IComponent> components)
        {
            var component = new ComponentsForTargetComponent(
                target,
                components);
            return component;
        }
    }
}
