namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface INewtonsoftJsonDeserializerFacade : INewtonsoftJsonDeserializer
    {
        void RegisterDefaultDeserializableConverter(NewtonsoftDeserializeDelegate converter);

        void Register(
            string serializableId,
            NewtonsoftDeserializeDelegate converter);
    }
}
