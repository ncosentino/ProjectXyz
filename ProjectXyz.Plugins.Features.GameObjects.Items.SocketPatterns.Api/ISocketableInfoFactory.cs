using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api
{
    public interface ISocketableInfoFactory
    {
        ISocketableInfo Create(IGameObject item);

        ISocketableInfo Create(
            IGameObject item,
            ICanBeSocketedBehavior canBeSocketedBehavior);
    }
}
