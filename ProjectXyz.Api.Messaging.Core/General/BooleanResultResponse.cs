using System;
using ProjectXyz.Api.Messaging.Interface.General;

namespace ProjectXyz.Api.Messaging.Core.General
{
    public sealed class BooleanResultResponse : IBooleanResultResponse
    {
        #region Properties
        public Guid RequestId { get; set; }

        public bool Result { get; set; }

        public Guid ErrorStringResourceId { get; set; }

        public Guid Id { get; set; }

        public string Type { get; set; }
        #endregion
    }
}
