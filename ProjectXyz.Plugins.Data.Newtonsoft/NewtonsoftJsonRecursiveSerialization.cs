using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonRecursiveSerialization
    {
        private readonly ICast _cast;
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly ISerializableIdToTypeConverterFacade _serializableIdToTypeConverterFacade;
        private readonly ISerializableConverterFacade _serializableConverterFacade;

        public NewtonsoftJsonRecursiveSerialization(
            ICast cast,
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade,
            ISerializableIdToTypeConverterFacade serializableIdToTypeConverterFacade,
            ISerializableConverterFacade serializableConverterFacade)
        {
            _cast = cast;
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _serializableIdToTypeConverterFacade = serializableIdToTypeConverterFacade;
            _serializableConverterFacade = serializableConverterFacade;
        }

        public ISerializable ToSerializable(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type)
        {
            var serializableId = _objectToSerializationIdConverterFacade.ConvertToSerializationId(objectToConvert);
            var converted = ToSerializable(
                serializer,
                objectToConvert,
                visited,
                type,
                serializableId);
            return converted;
        }

        public ISerializable ToSerializable(
            INewtonsoftJsonSerializer serializer,
            object objectToConvert,
            HashSet<object> visited,
            Type type,
            string serializableId)
        {
            var converted = _serializableConverterFacade.Convert(
                serializer,
                objectToConvert,
                visited,
                type,
                serializableId);
            return converted;
        }

        public object FromStream(
            INewtonsoftJsonDeserializer deserializer,
            Stream stream,
            string serializableId)
        {
            var type = _serializableIdToTypeConverterFacade.ConvertToType(serializableId);
            var readObject = deserializer.ReadObject(stream);
            if (!(readObject is JObject) && readObject != null)
            {
                return readObject;
            }

            var jsonObject = (JObject)readObject;
            var jsonPropertyNames = jsonObject == null
                ? new HashSet<string>()
                : jsonObject
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
                var forceEnumerableCasting = targetType.IsGenericType &&
                    typeof(IEnumerable).IsAssignableFrom(targetType);
                var forceArrayCasting = targetType.IsArray;
                if (forceEnumerableCasting || forceArrayCasting)
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
