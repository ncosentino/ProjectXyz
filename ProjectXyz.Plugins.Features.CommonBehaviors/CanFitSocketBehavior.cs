using ProjectXyz.Api.Framework;
using ProjectXyz.Game.Interface.Behaviors;
using ProjectXyz.Shared.Game.Behaviors;

namespace ProjectXyz.Plugins.Features.CommonBehaviors
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