using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class ConstructorDirectedSerializableConverter : IDiscoverableSerializableConverter
    {
        public bool CanConvert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            Type type,
            string serializableId) =>
                type.GetConstructors().Any(x => 
                    x.IsPublic &&    
                    x.GetParameters().Any());

        public ISerializable Convert(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            var constructorArgsAndTypes = type
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .Select(ctor => ctor
                    .GetParameters()
                    .ToDictionary(
                        x => x.Name,
                        x => x.ParameterType,
                        StringComparer.OrdinalIgnoreCase));

            var properties = type
                .GetProperties(
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.Instance)
                .ToDictionary(
                    x => x.Name,
                    x => x,
                    StringComparer.OrdinalIgnoreCase);

            var bestMatchCount = -1;
            var propertiesCache = new Dictionary<string, object>();
            Dictionary<string, Type> bestCtorEntry = null;
            foreach (var ctorEntry in constructorArgsAndTypes)
            {
                var currentMatch = 0;

                foreach (var paramEntry in ctorEntry)
                {
                    if (!properties.ContainsKey(paramEntry.Key))
                    {
                        continue;
                    }

                    object propertyValue;
                    if (!propertiesCache.TryGetValue(
                        paramEntry.Key,
                        out propertyValue))
                    {
                        propertyValue = properties[paramEntry.Key].GetValue(objectToConvert);
                        propertiesCache[paramEntry.Key] = propertyValue;
                    }
                    
                    if (propertyValue != null)
                    {
                        currentMatch++;
                        propertiesCache[paramEntry.Key] = propertyValue;
                    }
                }

                if (currentMatch > bestMatchCount)
                {
                    bestMatchCount = currentMatch;
                    bestCtorEntry = ctorEntry;
                }
            }

            var expando = new ExpandoObject() as IDictionary<string, object>;
            foreach (var paramsEntry in bestCtorEntry)
            {
                propertiesCache.TryGetValue(paramsEntry.Key, out var propertyValue);
                if (propertyValue == null)
                {
                    continue;
                }

                var serializableProperty = !serializer.NeedsSerialization(propertyValue)
                    ? propertyValue
                    : serializer.GetAsSerializable(
                        propertyValue,
                        propertyValue.GetType(),
                        visited);
                expando.Add(
                    paramsEntry.Key,
                    serializableProperty);
            }

            return new Serializable(
                serializableId,
                expando);
        }
    }
}
