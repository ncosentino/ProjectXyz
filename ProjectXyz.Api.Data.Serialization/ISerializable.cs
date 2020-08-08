namespace ProjectXyz.Api.Data.Serialization
{
    public interface ISerializable
    {
        string SerializableId { get; }

        object Data { get; }
    }
}
