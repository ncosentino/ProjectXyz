using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.SocketPatterns
{
    public sealed class SocketableInfoFactory : ISocketableInfoFactory
    {
        public ISocketableInfo Create(IGameObject item) => Create(
            item,
            item.GetOnly<ICanBeSocketedBehavior>());

        public ISocketableInfo Create(
            IGameObject item,
            ICanBeSocketedBehavior canBeSocketedBehavior)
        {
            var socketableInfo = new SocketableInfo(
                item,
                canBeSocketedBehavior);
            return socketableInfo;
        }
    }
}
