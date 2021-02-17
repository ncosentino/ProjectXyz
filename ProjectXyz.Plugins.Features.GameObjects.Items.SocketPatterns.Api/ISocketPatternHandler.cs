using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ISocketPatternHandler
    {
        bool TryHandle(
            ISocketableInfo socketableInfo,
            out IGameObject newItem);
    }
}
