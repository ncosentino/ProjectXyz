using ProjectXyz.Api.Behaviors;
using ProjectXyz.Api.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface ICanFitSocketBehavior : IBehavior
    {
        IIdentifier SocketType { get; }

        int SocketSize { get; }
    }
}