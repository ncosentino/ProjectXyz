using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class SerializableDto : ISerializableDto
    {
        public SerializableDto(string attributeTypeId, ISerializableDtoData data)
        {
            SerializableId = attributeTypeId;
            Data = data;
        }

        public string SerializableId { get; }

        public ISerializableDtoData Data { get; }
    }
}