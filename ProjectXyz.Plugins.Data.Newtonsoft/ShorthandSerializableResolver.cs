using System;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using ProjectXyz.Api.Data.Serialization;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class ShorthandSerializableResolver : DefaultContractResolver
    {
        private readonly IJsonSerializationSettings _settings;

        public ShorthandSerializableResolver(IJsonSerializationSettings settings)
        {
            _settings = settings;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (typeof(ISerializable).IsAssignableFrom(property.DeclaringType))
            {
                if (property.PropertyName.Equals(
                    nameof(ISerializable.SerializableId),
                    StringComparison.OrdinalIgnoreCase))
                {
                    property.PropertyName = _settings.SerializableIdPropertySerializedName;
                }
                else if (property.PropertyName.Equals(
                    nameof(ISerializable.Data),
                    StringComparison.OrdinalIgnoreCase))
                {
                    property.PropertyName = _settings.DataPropertySerializedName;
                }
            }

            return property;
        }
    }
}
