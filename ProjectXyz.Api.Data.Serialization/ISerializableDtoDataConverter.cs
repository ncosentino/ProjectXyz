namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableDtoDataConverter
    {
        ISerializableDtoData Convert<TSerializable>(TSerializable serializable);
    }
}