using System;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Api
{
    public interface IObservableSizeBehavior : IReadOnlySizeBehavior
    {
        event EventHandler<EventArgs> SizeChanged;
    }
}