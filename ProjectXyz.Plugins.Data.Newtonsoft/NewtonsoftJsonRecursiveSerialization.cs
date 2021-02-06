using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework.Collections;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonRecursiveSerialization
    {
        private readonly ICast _cast;
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly ISerializableIdToTypeConverterFacade _serializableIdToTypeConverterFacade;

        public NewtonsoftJsonRecursiveSerialization(
            ICast cast,
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade,
            ISerializableIdToTypeConverterFacade serializableIdToTypeConverterFacade)
        {
            _cast = cast;
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _serializableIdToTypeConverterFacade = serializableIdToTypeConverterFacade;
        }

        public ISerializable ToSerializable(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type)
        {
            var serialiableId = _objectToSerializationIdConverterFacade.ConvertToSerializationId(objectToConvert);
            if (!serializer.NeedsSerialization(type))
            {
                return new Serializable(
                    serialiableId,
                    objectToConvert);
            }

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                return new Serializable(
                    serialiableId,
                    ((IEnumerable)objectToConvert)
                        .Cast<object>()
                        .Select(x => serializer.GetAsSerializable(x, visited)));
            }

            var properties = type.GetProperties(
                BindingFlags.Public |
                BindingFlags.GetProperty |
                BindingFlags.Instance);

            var expando = new ExpandoObject() as IDictionary<string, object>;
            foreach (var property in properties)
            {
                var serializableProperty = !serializer.NeedsSerialization(property.PropertyType)
                    ? property.GetValue(objectToConvert)
                    : serializer.GetAsSerializable(
                        property.GetValue(objectToConvert),
                        visited);
                expando.Add(
                    property.Name,
                    serializableProperty);
            }

            return new Serializable(
                serialiableId,
                expando);
        }

        public object FromStream(
            INewtonsoftJsonDeserializer deserializer,
            Stream stream,
            string serializableId)
        {
            var type = _serializableIdToTypeConverterFacade.ConvertToType(serializableId);
            var jsonObject = deserializer.ReadObject(stream);
            var jsonPropertyNames = jsonObject
                .Children()
                .Select(x => x.Path)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
            var bestMatchingConstructors = GetBestConstructors(
                type,
                jsonPropertyNames)
                .ToArray();
            if (!bestMatchingConstructors.Any())
            {
                throw new InvalidOperationException(
                    $"No constructor found for '{type}'.");
            }

            IReadOnlyCollection<string> unsetPropertyNames = null;
            object instance = null;
            foreach (var constructor in bestMatchingConstructors)
            {
                if (TryCreateInstance(
                    deserializer,
                    jsonObject,
                    jsonPropertyNames,
                    constructor,
                    out unsetPropertyNames,
                    out instance))
                {
                    break;
                }
            }

            if (instance == null)
            {
                throw new InvalidOperationException(
                    $"Could not create instance of type '{type}'.");
            }

            if (unsetPropertyNames.Any())
            {
                var settableProperties = instance
                    .GetType()
                    .GetProperties(
                        BindingFlags.Instance |
                        BindingFlags.Public |
                        BindingFlags.NonPublic |
                        BindingFlags.SetProperty)
                    .Where(p => unsetPropertyNames.Contains(p.Name) && p.SetMethod != null)
                    .ToArray();

                foreach (var property in settableProperties)
                {
                    var jsonPropertyValue = jsonObject.GetValue(
                        property.Name,
                        StringComparison.OrdinalIgnoreCase);
                    var convertedValue = ConvertJsonValue(
                        deserializer,
                        property.PropertyType,
                        jsonPropertyValue);
                    property.SetValue(instance, convertedValue);
                }
            }

            return instance;
        }

        private IEnumerable<ConstructorInfo> GetBestConstructors(
            Type type,
            HashSet<string> jsonPropertyNames)
        {
            var sortedConstructors = type
                .GetConstructors()
                .Select(c => new
                {
                    Constructor = c,
                    Parameters = c.GetParameters(),
                })
                .OrderByDescending(x => x.Parameters.Length)
                .ThenByDescending(x => x.Parameters.Count(p => p.ParameterType.IsValueType || p.ParameterType == typeof(string)));

            // start with fully matching parameters
            foreach (var constructor in sortedConstructors
                .Where(x => x.Parameters.Select(p => p.Name).All(p => jsonPropertyNames.Contains(p)))
                .Select(x => x.Constructor))
            {
                yield return constructor;
            }

            foreach (var constructor in sortedConstructors
                .Where(x => !(x.Parameters.Select(p => p.Name).All(p => jsonPropertyNames.Contains(p))))
                .OrderByDescending(x => x.Parameters.Count(p => jsonPropertyNames.Contains(p.Name)))
                .Select(x => x.Constructor))
            {
                yield return constructor;
            }
        }

        private bool TryCreateInstance(
            INewtonsoftJsonDeserializer deserializer,
            JObject jsonObject,
            IReadOnlyCollection<string> propertyNames,
            ConstructorInfo constructor,
            out IReadOnlyCollection<string> unsetPropertyNames,
            out object instance)
        {
            var constructorParameters = new List<object>();
            var usedProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            try
            {
                foreach (var requiredParameter in constructor.GetParameters())
                {
                    bool jsonHasProperty = jsonObject.TryGetValue(
                        requiredParameter.Name,
                        StringComparison.OrdinalIgnoreCase,
                        out var jsonPropertyValue);
                    var parameterValue = !jsonHasProperty || jsonPropertyValue == null
                        ? null
                        : ConvertJsonValue(
                        deserializer,
                        requiredParameter.ParameterType,
                        jsonPropertyValue);
                    constructorParameters.Add(parameterValue);

                    if (jsonHasProperty)
                    {
                        usedProperties.Add(requiredParameter.Name);
                    }
                }

                var constructorParamsArray = constructorParameters.ToArray();
                instance = constructor.Invoke(constructorParamsArray);
                unsetPropertyNames = propertyNames
                    .Where(x => !usedProperties.Contains(x))
                    .ToArray();
                return true;
            }
            catch
            {
                instance = null;
                unsetPropertyNames = null;
                return false;
            }
        }

        private object ConvertJsonValue(
            INewtonsoftJsonDeserializer deserializer,
            Type targetType,
            JToken jsonPropertyValue)
        {
            if (jsonPropertyValue is JObject)
            {
                var convertedValue = deserializer.Deserialize<object>((JObject)jsonPropertyValue);
                if (targetType.IsGenericType &&
                    typeof(IEnumerable).IsAssignableFrom(targetType))
                {
                    return _cast.ToType(
                        convertedValue,
                        targetType);
                }

                return convertedValue;
            }
            
            if (jsonPropertyValue is JValue)
            {
                return _cast.ToType(
                    ((JValue)jsonPropertyValue).Value,
                    targetType);
            }
            
            throw new NotSupportedException(
                $"Unsupported JSON property type '{jsonPropertyValue.GetType()}'.");
        }
    }
}
