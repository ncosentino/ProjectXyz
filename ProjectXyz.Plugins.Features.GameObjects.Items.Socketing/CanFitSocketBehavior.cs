using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing
{
    public sealed class CanFitSocketBehavior :
        BaseBehavior,
        ICanFitSocketBehavior
    {
        public CanFitSocketBehavior(
            IIdentifier socketType,
            int socketSize)
        {
            SocketType = socketType;
            SocketSize = socketSize;
        }

        public IIdentifier SocketType { get; }

        public int SocketSize { get; }
    }
}