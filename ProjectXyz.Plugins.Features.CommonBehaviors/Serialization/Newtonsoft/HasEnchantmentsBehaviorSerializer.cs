using System;
using System.Linq;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Serialization.Newtonsoft
{
    public sealed class HasEnchantmentsBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly IHasEnchantmentsBehaviorFactory _hasEnchantmentsBehaviorFactory;

        public HasEnchantmentsBehaviorSerializer(IHasEnchantmentsBehaviorFactory hasEnchantmentsBehaviorFactory)
        {
            _hasEnchantmentsBehaviorFactory = hasEnchantmentsBehaviorFactory;
        }

        public Type TypeToRegisterFor { get; } = typeof(HasEnchantmentsBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonArray = (JArray)deserializer.ReadObject(stream);
            var enchantments = jsonArray.Select(x => deserializer.Deserialize<IGameObject>((JObject)x));
            var hasEnchantmentsBehavior = _hasEnchantmentsBehaviorFactory.Create();
            hasEnchantmentsBehavior.AddEnchantments(enchantments);
            return hasEnchantmentsBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var hasEnchantmentsBehavior = (HasEnchantmentsBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                hasEnchantmentsBehavior
                    .Enchantments
                    .Select(x => serializer.GetObjectToSerialize(x, visited)));
            return serializable;
        };
    }
}