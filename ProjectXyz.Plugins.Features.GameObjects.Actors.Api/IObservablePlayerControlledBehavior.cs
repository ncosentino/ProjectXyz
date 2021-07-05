using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Actors.Api
{
    public interface IObservablePlayerControlledBehavior : IReadOnlyPlayerControlledBehavior
    {
        event EventHandler<EventArgs> IsActiveChanged;
    }
}
