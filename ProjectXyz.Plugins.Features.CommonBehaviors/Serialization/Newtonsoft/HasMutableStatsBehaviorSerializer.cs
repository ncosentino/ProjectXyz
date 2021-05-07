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

        public HasMutableStatsBehaviorSerializer(IHasMutableStatsBehaviorFactory hasMutableStatsBehaviorFactory)
        {
            _hasMutableStatsBehaviorFactory = hasMutableStatsBehaviorFactory;
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
                    var statId = int.TryParse(kvp.Key, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericId)
                        ? (IIdentifier)new IntIdentifier(numericId)
                        : (IIdentifier)new StringIdentifier(kvp.Key);

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