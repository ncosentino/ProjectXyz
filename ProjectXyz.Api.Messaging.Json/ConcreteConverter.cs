using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using ProjectXyz.Utilities;

namespace ProjectXyz.Api.Messaging.Json
{
    public sealed class ConcreteConverter : JsonConverter
    {
        #region Fields
        private readonly Dictionary<Type, Type> _concreteTypeCache;
        private readonly ITypeMapper _typeMapper;
        #endregion

        #region Constructors
        private ConcreteConverter(ITypeMapper typeMapper)
        {
            _concreteTypeCache = new Dictionary<Type, Type>();
            _typeMapper = typeMapper;
        }
        #endregion

        #region Methods
        public static JsonConverter Create(ITypeMapper typeMapper)
        {
            var converter = new ConcreteConverter(typeMapper);
            return converter;
        }

        public override bool CanConvert(Type objectType)
        {
            if (!objectType.IsAbstract && !objectType.IsInterface)
            {
                return false;
            }

            if (_concreteTypeCache.ContainsKey(objectType))
            {
                return true;
            }

            var concreteType = _typeMapper.GetConcreteType(objectType);
            if (concreteType == null)
            {
                return false;
            }

            _concreteTypeCache[objectType] = concreteType;
            return true;
        }

        public override object ReadJson(
            JsonReader reader,
            Type objectType, 
            object existingValue, 
            JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, _concreteTypeCache[objectType]);
        }

        public override void WriteJson(
            JsonWriter writer,
            object value, 
            JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
        #endregion
    }
}
