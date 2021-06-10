using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class SocketPatternHandlerFacade : ISocketPatternHandlerFacade
    {
        private Lazy<IReadOnlyCollection<ISocketPatternHandler>> _lazyHandlers;

        public SocketPatternHandlerFacade(Lazy<IEnumerable<IDiscoverableSocketPatternHandler>> handlers)
        {
            _lazyHandlers = new Lazy<IReadOnlyCollection<ISocketPatternHandler>>(() =>
                handlers.Value.ToArray());
        }

        public bool TryHandle(
            IFilterContext filterContext,
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            IGameObject resultingItem = null;
            Parallel.ForEach(_lazyHandlers.Value, (handler, handlersLoopState, _) =>
            {
                if (!handler.TryHandle(
                    filterContext,
                    socketableInfo,
                    out var handlerOutput))
                {
                    return;
                }

                if (!handlersLoopState.IsStopped)
                {
                    handlersLoopState.Stop();
                    resultingItem = handlerOutput;
                }
            });

            newItem = resultingItem;
            return newItem != null;
        }
    }
}
