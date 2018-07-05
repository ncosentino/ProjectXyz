namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableDtoDataConverterRegistrar
    {
        void Register(
            string type,
            ISerializableDtoDataConverter serializableDtoDataConverter);
    }
}