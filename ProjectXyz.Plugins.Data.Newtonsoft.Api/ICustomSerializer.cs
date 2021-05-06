namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface ICustomSerializer
    {
        NewtonsoftDeserializeDelegate Deserializer { get; }

        ConvertToSerializableDelegate ConvertToSerializable { get; }
    }
}
