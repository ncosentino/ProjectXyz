namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface IDiscoverableSerializableDtoDataConverter : ISerializableDtoDataConverter
    {
        string DeserializableType { get; }
    }
}