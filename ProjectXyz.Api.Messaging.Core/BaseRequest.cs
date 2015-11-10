using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core
{
    public abstract class BaseRequest : IRequest
    {
        #region Properties
        public Guid Id { get; set; }
        #endregion
    }
}
