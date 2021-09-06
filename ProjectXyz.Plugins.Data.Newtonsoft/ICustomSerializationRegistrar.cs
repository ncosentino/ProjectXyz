using ProjectXyz.Plugins.Data.Newtonsoft.Api;

namespace ProjectXyz.Plugins.Data.Newtonsoft
{
    public interface ICustomSerializationRegistrar
    {
        void RegisterDeserializers(INewtonsoftJsonDeserializerFacade deserializerFacade);
        void RegisterSerializers(INewtonsoftJsonSerializerFacade serializerFacade);
    }
}