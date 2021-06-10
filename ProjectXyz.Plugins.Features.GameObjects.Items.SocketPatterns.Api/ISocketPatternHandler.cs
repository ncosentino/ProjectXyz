using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.Filtering.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ISocketPatternHandler
    {
        bool TryHandle(
            IFilterContext filterContext,
            ISocketableInfo socketableInfo,
            out IGameObject newItem);
    }
}
