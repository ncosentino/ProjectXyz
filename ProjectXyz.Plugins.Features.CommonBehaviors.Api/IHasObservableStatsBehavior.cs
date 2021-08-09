using System;

using ProjectXyz.Plugins.Features.Stats;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IHasObservableStatsBehavior : IHasReadOnlyStatsBehavior
    {
        event EventHandler<StatChangedEventArgs> BaseStatChanged;
    }
}