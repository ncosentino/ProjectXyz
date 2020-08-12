namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableIdToTypeConverterFacade : ISerializableIdToTypeConverter
    {
        void Register(string serializableId, ISerializableIdToTypeConverter converter);
    }
}
