using System;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IObservablePositionBehavior : IReadOnlyPositionBehavior
    {
        event EventHandler<EventArgs> PositionChanged;
    }
}