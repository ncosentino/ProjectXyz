using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Game.Interface.Behaviors
{
    public interface ICanFitSocketBehavior : IBehavior
    {
        IIdentifier SocketType { get; }

        int SocketSize { get; }
    }
}