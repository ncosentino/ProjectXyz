namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializableConverter
    {
        TSerializable Convert<TSerializable>(ISerializableDtoData dto);

        ISerializableDtoData ConvertBack<TSerializable>(TSerializable serializable);
    }
}