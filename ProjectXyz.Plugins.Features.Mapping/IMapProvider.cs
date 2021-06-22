using System;

using ProjectXyz.Api.GameObjects;

namespace ProjectXyz.Plugins.Features.Mapping
{
    public interface IMapProvider
    {
        event EventHandler<EventArgs> MapChanging;

        event EventHandler<EventArgs> MapChanged;

        event EventHandler<EventArgs> MapPopulated;

        IGameObject ActiveMap { get; }

        IPathFinder PathFinder { get; }
    }
}