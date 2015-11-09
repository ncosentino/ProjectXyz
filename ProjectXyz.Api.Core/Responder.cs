using System;
using System.Collections.Generic;
using System.IO;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Core
{
    public sealed class Responder : IResponder
    {
        #region Fields
        private readonly IResponseFactory _responseFactory;
        private readonly IResponseSender _responseSender;
        #endregion

        #region Constructors
        private Responder(
            IResponseFactory responseFactory,
            IResponseSender responseSender)
        {
            _responseFactory = responseFactory;
            _responseSender = responseSender;
        }
        #endregion

        #region Methods
        public static IResponder Create(
            IResponseFactory responseFactory,
            IResponseSender responseSender)
        {
            var responder = new Responder(
                responseFactory,
                responseSender);
            return responder;
        }

        public void Respond<TResponse>(Guid requestId)
            where TResponse : IResponse
        {
            Respond<TResponse>(requestId, _ => { });
        }

        public void Respond<TResponse>(Guid requestId, Action<TResponse> callback)
            where TResponse : IResponse
        {
            var response = _responseFactory.Create(requestId, callback);
            _responseSender.Send(response);
        }
        #endregion
    }
}
