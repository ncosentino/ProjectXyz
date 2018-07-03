namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public interface ISerializableDtoDataConverterRegistrar
    {
        void Register(
            string type,
            ISerializableDtoDataConverter serializableDtoDataConverter);
    }
}