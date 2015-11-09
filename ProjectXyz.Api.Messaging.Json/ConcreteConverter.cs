using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace ProjectXyz.Api.Messaging.Json
{
    public sealed class ConcreteConverter : JsonConverter
    {
        #region Fields
        private readonly Dictionary<Type, Type> _concreteTypeCache;
        private readonly Type[] _possibleTypeCache;
        #endregion

        #region Constructors
        private ConcreteConverter(Func<IEnumerable<Type>> getTypesCallback)
        {
            _concreteTypeCache = new Dictionary<Type, Type>();
            _possibleTypeCache = getTypesCallback
                .Invoke()
                .Where(x => !x.IsAbstract &&
                    !x.IsInterface &&
                    x.GetConstructors().FirstOrDefault(c => c.GetParameters().Length == 0) != null)
                .ToArray();
        }
        #endregion

        #region Methods
        public static JsonConverter Create(Func<IEnumerable<Type>> getTypesCallback)
        {
            var converter = new ConcreteConverter(getTypesCallback);
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

            var concreteType = _possibleTypeCache.FirstOrDefault(objectType.IsAssignableFrom);
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
