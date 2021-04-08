using System;

namespace ProjectXyz.Plugins.Features.Mapping.Api
{
    public interface IMapProvider
    {
        event EventHandler<EventArgs> MapChanging;

        event EventHandler<EventArgs> MapChanged;

        event EventHandler<EventArgs> MapPopulated;

        IMap ActiveMap { get; }

        IPathFinder PathFinder { get; }
    }
}