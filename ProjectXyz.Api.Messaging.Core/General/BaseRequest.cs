using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.General
{
    public abstract class BaseRequest : IRequest
    {
        #region Properties
        public Guid Id { get; set; }
        #endregion
    }
}
