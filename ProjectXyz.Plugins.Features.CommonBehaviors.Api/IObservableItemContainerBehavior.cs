using System;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IObservableItemContainerBehavior : IReadOnlyItemContainerBehavior
    {
        event EventHandler<ItemsChangedEventArgs> ItemsChanged;
    }
}