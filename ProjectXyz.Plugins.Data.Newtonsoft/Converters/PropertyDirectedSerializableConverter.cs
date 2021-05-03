using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class PropertyDirectedSerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) => 
                !type.GetConstructors().Any(x => 
                    x.IsPublic &&
                    x.GetParameters().Any()) &&
                type.GetProperties().Any(x => x.CanWrite);

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            var properties = type
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.Instance)
                // NOTE: only write out properties we can set as deserialize time
                .Where(x => x.CanWrite);

            var expando = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(objectToConvert);
                if (propertyValue == null)
                {
                    continue;
                }

                var serializableProperty = serializer.GetObjectToSerialize(
                    propertyValue,
                    visited);
                expando.Add(
                    property.Name,
                    serializableProperty);
            }

            return new Serializable(
                serializableId,
                expando);
        }
    }
}
