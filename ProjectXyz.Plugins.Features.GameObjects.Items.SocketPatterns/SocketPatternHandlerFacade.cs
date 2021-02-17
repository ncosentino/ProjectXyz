using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class SocketPatternHandlerFacade : ISocketPatternHandlerFacade
    {
        private IReadOnlyCollection<ISocketPatternHandler> _handlers;

        public SocketPatternHandlerFacade(IEnumerable<IDiscoverableSocketPatternHandler> handlers)
        {
            _handlers = handlers.ToArray();
        }

        public bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem)
        {
            newItem = null;
            foreach (var handler in _handlers)
            {
                if (handler.TryHandle(socketableInfo, out newItem))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
