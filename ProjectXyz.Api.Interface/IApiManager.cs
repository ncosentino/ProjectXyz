using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectXyz.Api.Interface
{
    public interface IApiManager
    {
        #region Properties
        INotifier Notifier { get; }

        IResponder Responder { get; }

        IRequestRegistrar RequestRegistrar { get; }
        #endregion
    }
}
