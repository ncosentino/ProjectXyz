namespace ProjectXyz.Api.Data.Serialization
{
    public interface IObjectToSerializationIdConverter
    {
        string ConvertToSerializationId(object obj);
    }
}
