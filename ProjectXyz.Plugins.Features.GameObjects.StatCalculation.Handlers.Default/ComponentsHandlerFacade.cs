using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Framework.Entities;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Api.Handlers;

namespace ProjectXyz.Plugins.Features.GameObjects.StatCalculation.Handlers.Default
{
    public sealed class ComponentsHandlerFacade : IComponentsHandlerFacade
    {
        private readonly IReadOnlyCollection<IDiscoverableComponentsHandler> _discoverableComponentsHandlers;

        public ComponentsHandlerFacade(IEnumerable<IDiscoverableComponentsHandler> discoverableComponentsHandlers)
        {
            _discoverableComponentsHandlers = discoverableComponentsHandlers.ToArray();
        }

        public IEnumerable<IComponent> HandleComponents(
            IGameObject gameObject,
            IReadOnlyCollection<IComponent> components) =>
            _discoverableComponentsHandlers.SelectMany(x => x.HandleComponents(
                gameObject,
                components));
    }
}
