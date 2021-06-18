using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Api.GameObjects;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;
using ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Api;

namespace ProjectXyz.Plugins.Features.GameObjects.Items.Socketing.Serialization.Newtonsoft
{
    public sealed class CanBeSocketedBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly ICanBeSocketedBehaviorFactory _canBeSocketedBehaviorFactory;
        private readonly IIdentifierConverter _identifierConverter;

        public CanBeSocketedBehaviorSerializer(
            ICanBeSocketedBehaviorFactory canBeSocketedBehaviorFactory,
            IIdentifierConverter identifierConverter)
        {
            _canBeSocketedBehaviorFactory = canBeSocketedBehaviorFactory;
            _identifierConverter = identifierConverter;
        }

        public Type TypeToRegisterFor { get; } = typeof(CanBeSocketedBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            var totalSocketTypes = jsonObject["TotalSocketTypes"]
                .ToObject<Dictionary<string, int>>()
                .SelectMany(x => Enumerable.Repeat(
                    _identifierConverter.Convert(x.Key),
                    x.Value));
            var canBeSocketedBehavior = _canBeSocketedBehaviorFactory.Create(totalSocketTypes);
            
            var socketedItems = jsonObject["SocketedItems"]
                .Select(x => deserializer.Deserialize<IGameObject>((JObject)x));
            foreach (var item in socketedItems)
            {
                if (!canBeSocketedBehavior.Socket(item.GetOnly<ICanFitSocketBehavior>()))
                {
                    throw new InvalidOperationException(
                        $"Cannot deserialize '{serializableId}' because " +
                        $"'{item}' could not be socketed into " +
                        $"'{canBeSocketedBehavior}'. Please see the json:\r\n" +
                        $"{jsonObject}");
                }
            }
            
            return canBeSocketedBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var canBeSocketedBehavior = (CanBeSocketedBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                new
                {
                    TotalSocketTypes = canBeSocketedBehavior.TotalSockets,
                    SocketedItems = canBeSocketedBehavior
                        .OccupiedSockets
                        .Select(x => serializer.GetObjectToSerialize(
                            x.Owner,
                            visited)),
                });
            return serializable;
        };
    }
}