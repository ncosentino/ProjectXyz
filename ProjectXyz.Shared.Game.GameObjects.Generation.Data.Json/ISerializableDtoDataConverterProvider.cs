namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface ISerializableDtoDataConverterProvider
    {
        bool TryGet(
            string name,
            out ISerializableDtoDataConverter serializableDtoDataConverter);
    }
}