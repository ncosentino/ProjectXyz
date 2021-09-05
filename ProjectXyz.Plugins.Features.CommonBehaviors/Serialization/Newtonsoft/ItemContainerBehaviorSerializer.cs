using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Serialization.Newtonsoft
{
    public sealed class ItemContainerBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly IIdentifierConverter _identifierConverter;

        public ItemContainerBehaviorSerializer(IIdentifierConverter identifierConverter)
        {
            _identifierConverter = identifierConverter;
        }

        public Type TypeToRegisterFor { get; } = typeof(ItemContainerBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            ////var containerId = jsonObject["ContainerId"]
            ////    .ToObject<JObject[]>()
            ////    .Select(x => (JValue)x["Identifier"])
            ////    .Select(x => _identifierConverter.Convert(x.Value<string>()));
            var containerId = new StringIdentifier("FIXME");
            var items = jsonObject["Items"]
                .ToObject<object[]>()
                .Select(x => deserializer.Deserialize<IGameObject>((JObject)x));

            var itemContainerBehavior = new ItemContainerBehavior(containerId);
            foreach (var item in items)
            {
                itemContainerBehavior.TryAddItem(item);
            }

            return itemContainerBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var itemContainerBehavior = (ItemContainerBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                new
                {
                    ContainerId = itemContainerBehavior.ContainerId,
                    Items = itemContainerBehavior
                        .Items
                        .Select(x => serializer.GetObjectToSerialize(
                            x,
                            visited))
                        .ToArray(),
                });
            return serializable;
        };
    }
}