using System;
using System.Collections.Generic;
using System.Globalization;

using Newtonsoft.Json.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;
using ProjectXyz.Plugins.Features.CommonBehaviors.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Features.CommonBehaviors.Serialization.Newtonsoft
{
    public sealed class HasMutableStatsBehaviorSerializer : IDiscoverableCustomSerializer
    {
        private readonly IHasMutableStatsBehaviorFactory _hasMutableStatsBehaviorFactory;
        private readonly IIdentifierConverter _identifierConverter;

        public HasMutableStatsBehaviorSerializer(
            IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory,
            IIdentifierConverter identifierConverter)
        {
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
            _identifierConverter = identifierConverter;
        }

        public Type TypeToRegisterFor { get; } = typeof(HasMutableStatsBehavior);

        public NewtonsoftDeserializeDelegate Deserializer => (
            deserializer,
            stream,
            serializableId) =>
        {
            var jsonObject = (JObject)deserializer.ReadObject(stream);
            var statValues = jsonObject.ToObject<Dictionary<string, double>>();
            var hasMutableStatsBehavior = _hasMutableStatsBehaviorFactory.Create();
            hasMutableStatsBehavior.MutateStats(stats =>
            {
                foreach (var kvp in statValues)
                {
                    var statId = _identifierConverter.Convert(kvp.Key);
                    stats[statId] = kvp.Value;
                }
            });

            return hasMutableStatsBehavior;
        };

        public ConvertToSerializableDelegate ConvertToSerializable => (
            serializer,
            objectToConvert,
            visited,
            type,
            serializableId) =>
        {
            var hasMutableStatsBehavior = (HasMutableStatsBehavior)objectToConvert;
            var serializable = new Serializable(
                serializableId,
                hasMutableStatsBehavior.BaseStats);
            return serializable;
        };
    }
}