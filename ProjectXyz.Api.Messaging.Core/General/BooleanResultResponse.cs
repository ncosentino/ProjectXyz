using System;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.General
{
    public sealed class BooleanResultResponse : BaseResponse
    {
        #region Properties
        public bool Result { get; set; }

        public Guid ErrorStringResourceId { get; set; }
        
        public string Type { get; set; }
        #endregion
    }
}
