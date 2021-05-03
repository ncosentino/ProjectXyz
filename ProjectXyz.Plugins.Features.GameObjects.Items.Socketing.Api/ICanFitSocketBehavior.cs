using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects.Behaviors;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api
{
    public interface ICanFitSocketBehavior : IBehavior
    {
        IIdentifier SocketType { get; }

        int SocketSize { get; }
    }
}