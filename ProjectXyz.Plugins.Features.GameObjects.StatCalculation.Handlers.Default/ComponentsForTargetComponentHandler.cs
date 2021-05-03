using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class ComponentsForTargetComponentHandler : IDiscoverableComponentsHandler
    {
        public IEnumerable<IComponent> HandleComponents(
            IGameObject gameObject,
            IReadOnlyCollection<IComponent> components)
        {
            foreach (var component in components
                .TakeTypes<IComponentsForTargetComponent>())
            {
                if (gameObject != component.Target)
                {
                    continue;
                }

                foreach (var additionalComponent in component.Components)
                {
                    yield return additionalComponent;
                }
            }
        }
    }
}
