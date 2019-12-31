using Newtonsoft.Json.Linq;
using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Shared.Game.GameObjects.Generation.Attributes;

namespace ProjectXyz.Shared.Game.GameObjects.Generation.Data.Json.Attributes
{
    public sealed class StringSerializableDtoDataConverter : IDiscoverableSerializableDtoDataConverter
    {
        public string DeserializableType { get; } = typeof(StringGeneratorAttributeValue).FullName;

        public ISerializableDtoData Convert<TSerializable>(TSerializable serializable)
        {
            Contract.Requires(
                serializable is JObject,
                $"'{this}' expects to only convert '{typeof(JObject)}' " +
                $"but '{serializable.GetType()}' was provided.");

            var jsonObject = (JObject)(object)serializable;
            var value = jsonObject
                .GetValue("Value")
                .Value<string>();
            var dto = new StringSerializableDtoValue()
            {
                Value = value,
            };
            return dto;
        }
    }
}