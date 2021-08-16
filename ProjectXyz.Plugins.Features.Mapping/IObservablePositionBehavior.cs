using System;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IObservablePositionBehavior : IReadOnlyPositionBehavior
    {
        event EventHandler<EventArgs> PositionChanged;
    }
}