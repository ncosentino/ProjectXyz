using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface.Messaging;

namespace ProjectXyz.Api.Interface
{
    public sealed class RequestPublishedEventArgs : EventArgs
    {
        #region Constructors
        public RequestPublishedEventArgs(IRequest request)
        {
            Request = request;
        }
        #endregion

        #region Properties
        public IRequest Request { get; }
        #endregion
    }
}
