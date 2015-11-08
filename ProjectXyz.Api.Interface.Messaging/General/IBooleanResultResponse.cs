using System;

namespace ProjectXyz.Api.Interface.Messaging.General
{
    public interface IBooleanResultResponse : IResponse
    {
        #region Properties
        bool Result { get; }

        Guid ErrorStringResourceId { get; }
        #endregion
    }
}
