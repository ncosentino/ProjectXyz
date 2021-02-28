using System;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapProvider
    {
        event EventHandler<EventArgs> MapChanged;

        IMap ActiveMap { get; }
    }
}