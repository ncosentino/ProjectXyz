using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core
{
    public sealed class ResponseFactory : IResponseFactory
    {
        #region Fields
        private readonly Func<Type, object> _createInstanceCallback;
        #endregion

        #region Constructors
        private ResponseFactory(Func<Type, object> createInstanceCallback)
        {
            _createInstanceCallback = createInstanceCallback;
        }
        #endregion

        #region Methods
        public static IResponseFactory Create(Func<Type, object> createInstanceCallback)
        {
            var factory = new ResponseFactory(createInstanceCallback);
            return factory;
        }

        public TResponse Create<TResponse>(
            Guid requestId, 
            Action<TResponse> callback)
            where TResponse : IResponse
        {
            var response = (TResponse)_createInstanceCallback.Invoke(typeof(TResponse));
            response.Id = Guid.NewGuid();
            response.RequestId = requestId;

            callback.Invoke(response);

            return response;
        }
        #endregion
    }
}
