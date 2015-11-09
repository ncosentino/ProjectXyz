using System;

namespace ProjectXyz.Api.Messaging.Interface.General
{
    public interface IBooleanResultResponse : IResponse
    {
        #region Properties
        bool Result { get; set; }

        Guid ErrorStringResourceId { get; set; }
        #endregion
    }
}
