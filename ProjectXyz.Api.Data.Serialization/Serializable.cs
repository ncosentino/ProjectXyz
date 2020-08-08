namespace ProjectXyz.Api.Data.Serialization
{
    public sealed class Serializable : ISerializable
    {
        public Serializable(
            string serializableId,
            object data)
        {
            SerializableId = serializableId;
            Data = data;
        }

        public string SerializableId { get; }

        public object Data { get; }
    }
}
