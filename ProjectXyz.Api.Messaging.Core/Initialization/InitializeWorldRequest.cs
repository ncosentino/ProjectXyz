using System;
using ProjectXyz.Api.Messaging.Core.General;
using ProjectXyz.Api.Messaging.Interface.Initialization;

namespace ProjectXyz.Api.Messaging.Core.Initialization
{
    public sealed class InitializeWorldRequest : 
        BaseRequest, 
        IInitializeWorldRequest
    {
        #region Properties
        public Guid PlayerId { get; set; }
        #endregion
    }
}
