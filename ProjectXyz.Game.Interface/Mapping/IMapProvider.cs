using System;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapProvider
    {
        event EventHandler<EventArgs> MapChanged;

        IMap ActiveMap { get; }
    }
}