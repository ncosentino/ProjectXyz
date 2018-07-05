namespace ProjectXyz.Api.Data.Serialization
{
    public interface IDiscoverableSerializableDtoDataConverter : ISerializableDtoDataConverter
    {
        string DeserializableType { get; }
    }
}