using Newtonsoft.Json.Linq;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json
{
    public sealed class StringSerializableDtoDataConverter : IDiscoverableSerializableDtoDataConverter
    {
        public string DeserializableType { get; } = typeof(StringGeneratorAttributeValue).FullName;

        public ISerializableDtoData Convert(JObject jsonObject)
        {
            var value = jsonObject
                .GetValue("Value")
                .Value<string>();
            var dto = new StringSerializableDtoValue()
            {
                Value =  value,
            };
            return dto;
        }
    }
}