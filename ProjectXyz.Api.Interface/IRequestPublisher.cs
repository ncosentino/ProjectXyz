using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Api.Interface
{
    public interface IRequestPublisher : IDisposable
    {
        #region Events
        event EventHandler<RequestPublishedEventArgs> Publish;
        #endregion
    }
}
