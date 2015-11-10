using System;
using System.Collections.Generic;
using System.Reflection;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Interface
{
    public interface IMessageDiscoverer
    {
        #region Methods
        IDictionary<string, Type> Discover<TPayload>(IEnumerable<Assembly> assemblies)
            where TPayload : IPayload;
        #endregion
    }
}