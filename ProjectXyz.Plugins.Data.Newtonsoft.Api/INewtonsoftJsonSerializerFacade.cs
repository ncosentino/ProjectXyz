using System;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface INewtonsoftJsonSerializerFacade : INewtonsoftJsonSerializer
    {
        void RegisterDefaultSerializableConverter(ConvertToSerializableDelegate converter);

        void Register(
            Type type,
            ConvertToSerializableDelegate converter);

        void RegisterTypeToIgnore(Type type);

        void RegisterAllowedType(Type type);
    }
}
