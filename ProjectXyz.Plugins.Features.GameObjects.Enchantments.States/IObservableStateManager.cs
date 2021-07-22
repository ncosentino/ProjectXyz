using System;

namespace ProjectXyz.Plugins.Features.GameObjects.Enchantments.States
{
    public interface IObservableStateManager : IReadOnlyStateManager
    {
        event EventHandler<StateChangedEventArgs> StateChanged;
    }
}
