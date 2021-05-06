using System;

namespace ProjectXyz.Plugins.Data.Newtonsoft.Api
{
    public interface IDiscoverableCustomSerializer : ICustomSerializer
    {
        Type TypeToRegisterFor { get; }
    }
}
