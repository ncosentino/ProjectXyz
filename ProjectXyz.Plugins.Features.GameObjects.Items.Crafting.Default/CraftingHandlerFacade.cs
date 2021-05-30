using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api.Attributes;
using ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Crafting.Default
{
    public sealed class CraftingHandlerFacade : ICraftingHandlerFacade
    {
        private readonly Lazy<IReadOnlyCollection<IDiscoverableCraftingHandler>> _lazyHandlers;

        public CraftingHandlerFacade(Lazy<IEnumerable<IDiscoverableCraftingHandler>> handlers)
        {
            _lazyHandlers = new Lazy<IReadOnlyCollection<IDiscoverableCraftingHandler>>(() =>
                handlers.Value.ToArray());
        }

        public bool TryHandle(
            IReadOnlyCollection<IFilterAttribute> filterAttributes,
            IReadOnlyCollection<IGameObject> ingredients,
            out IReadOnlyCollection<IGameObject> newItems)
        {
            IReadOnlyCollection<IGameObject> resultingItems = null;
            Parallel.ForEach(_lazyHandlers.Value, (handler, handlersLoopState, _) =>
            {
                if (!handler.TryHandle(
                    filterAttributes,
                    ingredients,
                    out var handlerOutput))
                {
                    return;
                }

                if (!handlersLoopState.IsStopped)
                {
                    handlersLoopState.Stop();
                    resultingItems = handlerOutput;
                }
            });

            newItems = resultingItems;
            return newItems != null;
        }
    }
}
