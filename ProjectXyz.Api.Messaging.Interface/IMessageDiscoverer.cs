using System;
using System.Collections.Generic;
using System.Reflection;

namespace ProjectXyz.Api.Messaging.Interface
{
    public interface IMessageDiscoverer
    {
        #region Methods
        IDictionary<string, Type> Discover<TPayload>(IEnumerable<Assembly> assemblies)
            where TPayload : IPayload;
        #endregion
    }
}