using System;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface;
using ProjectXyz.Utilities;

namespace ProjectXyz.Api.Core
{
    public sealed class ResponseFactory : IResponseFactory
    {
        #region Fields
        private readonly Func<Type, object> _createInstanceCallback;
        private readonly ITypeMapper _typeMapper;
        #endregion

        #region Constructors
        private ResponseFactory(
            ITypeMapper typeMapper,
            Func<Type, object> createInstanceCallback)
        {
            _typeMapper = typeMapper;
            _createInstanceCallback = createInstanceCallback;
        }
        #endregion

        #region Methods
        public static IResponseFactory Create(
            ITypeMapper typeMapper,
            Func<Type, object> createInstanceCallback)
        {
            var factory = new ResponseFactory(
                typeMapper,
                createInstanceCallback);
            return factory;
        }

        public TResponse Create<TResponse>(
            Guid requestId, 
            Action<TResponse> callback)
            where TResponse : IResponse
        {
            var concreteType = _typeMapper.GetConcreteType(typeof(TResponse));
            var response = (TResponse)_createInstanceCallback.Invoke(concreteType);
            response.Id = Guid.NewGuid();
            response.RequestId = requestId;

            callback.Invoke(response);

            return response;
        }
        #endregion
    }
}
