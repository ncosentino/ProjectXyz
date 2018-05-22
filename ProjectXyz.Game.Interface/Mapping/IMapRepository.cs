namespace ProjectXyz.Game.Interface.Mapping
{
    public interface IMapRepository
    {
        IMap LoadMap(string mapResourceId);
    }
}