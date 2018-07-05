namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableDtoDataConverterProvider
    {
        bool TryGet(
            string name,
            out ISerializableDtoDataConverter serializableDtoDataConverter);
    }
}