using System.Collections.Generic;

using ProjectXyz.Api.Data.Serialization;
using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public sealed class CustomSerializationRegistrar
    {
        private readonly IObjectToSerializationIdConverterFacade _objectToSerializationIdConverterFacade;
        private readonly INewtonsoftJsonDeserializerFacade _newtonsoftJsonDeserializerFacade;
        private readonly INewtonsoftJsonSerializerFacade _newtonsoftJsonSerializerFacade;

        public CustomSerializationRegistrar(
            IObjectToSerializationIdConverterFacade objectToSerializationIdConverterFacade, 
            INewtonsoftJsonDeserializerFacade newtonsoftJsonDeserializerFacade,
            INewtonsoftJsonSerializerFacade newtonsoftJsonSerializerFacade,
            IEnumerable<IDiscoverableCustomSerializer> customSerializers)
        {
            _objectToSerializationIdConverterFacade = objectToSerializationIdConverterFacade;
            _newtonsoftJsonDeserializerFacade = newtonsoftJsonDeserializerFacade;
            _newtonsoftJsonSerializerFacade = newtonsoftJsonSerializerFacade;

            foreach (var customSerializer in customSerializers)
            {
                _newtonsoftJsonSerializerFacade.Register(
                    customSerializer.TypeToRegisterFor,
                    customSerializer.ConvertToSerializable);

                var serializableId = _objectToSerializationIdConverterFacade
                    .ConvertToSerializationId(customSerializer.TypeToRegisterFor);
                _newtonsoftJsonDeserializerFacade.Register(
                    serializableId,
                    customSerializer.Deserializer);
            }
        }
    }
}
