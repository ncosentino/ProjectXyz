using System;

namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapManager : IMapProvider
    {
        void SwitchMap(string mapResourceId);
    }

    public interface IMapProvider
    {
        event EventHandler<EventArgs> MapChanged;

        IMap ActiveMap { get; }
    }
}