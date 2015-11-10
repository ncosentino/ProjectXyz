using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.Initialization
{
    public sealed class InitializeWorldRequest : BaseRequest
    {
        #region Properties
        public Guid PlayerId { get; set; }
        #endregion
    }
}
