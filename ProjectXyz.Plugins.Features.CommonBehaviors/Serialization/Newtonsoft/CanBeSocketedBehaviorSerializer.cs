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
    public sealed class CanEquipBehaviorSerializer : IDiscoverableCustomSerializer
    {
        public Type TypeToRegisterFor { get; } = typeof(CanEquipBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            var supportedEquipSlotIds = jsonObject["SupportedEquipSlotIds"]
                .ToObject<JObject[]>()
                .Select(x => (JValue)x["Identifier"])
                .Select(x => int.TryParse(x.Value<string>(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId)
                    ? new IntIdentifier(numericId)
                    : (IIdentifier)new StringIdentifier(x.Value<string>()));
            var equippedItems = jsonObject["EquippedItems"]
                .ToObject<Dictionary<string, object>>()
                .ToDictionary(
                    x => int.TryParse(x.Key, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId)
                        ? new IntIdentifier(numericId)
                        : (IIdentifier)new StringIdentifier(x.Key),
                    x => deserializer
                        .Deserialize<IGameObject>((JObject)x.Value)
                        .GetOnly<ICanBeEquippedBehavior>());

            var canEquipBehavior = new CanEquipBehavior(
                supportedEquipSlotIds,
                equippedItems);
            return canEquipBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var canEquipBehavior = (CanEquipBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                new
                {
                    SupportedEquipSlotIds = canEquipBehavior.SupportedEquipSlotIds,
                    EquippedItems = canEquipBehavior
                        .GetEquippedItemsBySlot()
                        .ToDictionary(
                            x => x.Item1,
                            x => serializer.GetObjectToSerialize(
                                x.Item2,
                                visited)),
                });
            return serializable;
        };
    }
}