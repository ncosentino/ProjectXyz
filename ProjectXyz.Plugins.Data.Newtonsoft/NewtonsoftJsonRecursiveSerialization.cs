using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json.Linq;

using NexusLabs.Framework;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Api.Framework;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;
using ProjectXyz.Shared.Framework;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class NewtonsoftJsonRecursiveSerialization
    {
        private readonly ICast _cast;
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly ISerializableIdToTypeConverterFacade _serializableIdToTypeConverterFacade;
        private readonly ISerializableConverterFacade _serializableConverterFacade;
        private readonly IIdentifierConverter _identifierConverter;

        public NewtonsoftJsonRecursiveSerialization(
            ICast cast,
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade,
            ISerializableIdToTypeConverterFacade serializableIdToTypeConverterFacade,
            ISerializableConverterFacade serializableConverterFacade,
            IIdentifierConverter identifierConverter)
        {
            _cast = cast;
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _serializableIdToTypeConverterFacade = serializableIdToTypeConverterFacade;
            _serializableConverterFacade = serializableConverterFacade;
            _identifierConverter = identifierConverter;
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

            var type = _serializableIdToTypeConverterFacade.ConvertToType(serializableId);
            if (type == null)
            {
                throw new InvalidOperationException(
                    $"The resulting type was null for serializable ID '{serializableId}'.");
            }

            if (typeof(IDictionary).IsAssignableFrom(type))
            {
                var uncastedDictionary = jsonObject.ToObject<IDictionary<string, object>>();
                if (type.IsGenericType && type.GenericTypeArguments.First() == typeof(IIdentifier))
                {
                    var newSourceDict = new Dictionary<IIdentifier, object>();
                    foreach (var kvp in uncastedDictionary)
                    {
                        if (!_identifierConverter.TryConvert(kvp.Key, out var key))
                        {
                            throw new InvalidOperationException(
                                $"Cannot convert '{kvp.Key}' to type " +
                                $"'{typeof(IIdentifier)}'. This is the expected " +
                                $"key format of the dictionary type '{type}'. " +
                                $"The JSON object could not be converted:\r\n" +
                                $"{jsonObject}");
                        }

                        newSourceDict[key] = kvp.Value;
                    }

                    var identifierCastedDictionary = _cast.ToType(newSourceDict, type);
                    return identifierCastedDictionary;
                }

                var castedDictionary = _cast.ToType(uncastedDictionary, type);
                return castedDictionary;
            }

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
            Exception lastConstructorException = null;
            foreach (var constructor in bestMatchingConstructors)
            {
                try
                {
                    instance = CreateInstance(
                        deserializer,
                        jsonObject,
                        jsonPropertyNames,
                        constructor,
                        out unsetPropertyNames);
                    break;
                }
                catch (Exception ex)
                {
                    lastConstructorException = ex;
                }
            }

            if (instance == null)
            {
                throw new InvalidOperationException(
                    $"Could not create instance of type '{type}'. See inner " +
                    $"exception for more details. Could not fulfill constructor " +
                    $"requirements given the JSON object:\r\n" +
                    $"{jsonObject}",
                    lastConstructorException);
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
                    
                    object convertedValue;
                    try
                    {
                        convertedValue = ConvertJsonValue(
                            deserializer,
                            property.PropertyType,
                            jsonPropertyValue);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException(
                            $"Could not convert JSON value '{jsonPropertyValue}' " +
                            $"to type '{property.PropertyType}' from JSON object:\r\n" +
                            $"{jsonObject}\r\n" +
                            $"\r\n" +
                            $"See inner exception for details.",
                            ex);
                    }

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

        private object CreateInstance(
            INewtonsoftJsonDeserializer deserializer,
            JObject jsonObject,
            IReadOnlyCollection<string> propertyNames,
            ConstructorInfo constructor,
            out IReadOnlyCollection<string> unsetPropertyNames)
        {
            var constructorParameters = new List<object>();
            var usedProperties = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

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
            var instance = constructor.Invoke(constructorParamsArray);
            unsetPropertyNames = propertyNames
                .Where(x => !usedProperties.Contains(x))
                .ToArray();
            return instance;
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
