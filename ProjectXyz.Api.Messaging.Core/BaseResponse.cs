using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core
{
    public abstract class BaseResponse : IResponse
    {
        #region Properties
        public Guid Id { get; set; }

        public Guid RequestId { get; set; }
        #endregion
    }
}
