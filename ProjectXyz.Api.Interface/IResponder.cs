using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface.Messaging;

namespace ProjectXyz.Api.Interface
{
    public interface IResponder
    {
        #region Methods
        void Respond<TResponse>(TResponse response)
            where TResponse : IResponse;
        #endregion
    }
}
