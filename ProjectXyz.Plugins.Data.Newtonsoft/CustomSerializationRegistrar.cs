using System;
using System.Collections.Generic;
using System.Linq;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class CustomSerializationRegistrar : ICustomSerializationRegistrar
    {
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly Lazy<IReadOnlyCollection<IDiscoverableCustomSerializer>> _lazyCustomSerializers;

        public CustomSerializationRegistrar(
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade,
            Lazy<IEnumerable<IDiscoverableCustomSerializer>> lazyCustomSerializers)
        {
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _lazyCustomSerializers = new Lazy<IReadOnlyCollection<IDiscoverableCustomSerializer>>(()
                => lazyCustomSerializers.Value.ToArray());
        }

        public void RegisterSerializers(INewtonsoftJsonSerializerFacade serializerFacade)
        {
            foreach (var customSerializer in _lazyCustomSerializers.Value)
            {
                serializerFacade.Register(
                    customSerializer.TypeToRegisterFor,
                    customSerializer.ConvertToSerializable);
            }
        }

        public void RegisterDeserializers(INewtonsoftJsonDeserializerFacade deserializerFacade)
        {
            foreach (var customSerializer in _lazyCustomSerializers.Value)
            {
                var serializableId = _objectToSerializationIdConverterFacade
                    .ConvertToSerializationId(customSerializer.TypeToRegisterFor);
                deserializerFacade.Register(
                    serializableId,
                    customSerializer.Deserializer);
            }
        }
    }
}
