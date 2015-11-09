using System.IO;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface;
using System.Collections.Generic;
using System;

namespace ProjectXyz.Api.Amqp
{
    public sealed class ResponseSender : IResponseSender
    {
        #region Fields
        private readonly Dictionary<Type, string> _responseTypeMapping;
        private readonly IResponseWriter _responseWriter;
        private readonly IChannelWriter _channelWriter;
        #endregion

        #region Constructors
        private ResponseSender(
            IResponseWriter responseWriter,
            IChannelWriter channelWriter,
            IDictionary<Type, string> responseTypeMapping)
        {
            _responseTypeMapping = new Dictionary<Type, string>(responseTypeMapping);
            _responseWriter = responseWriter;
            _channelWriter = channelWriter;
        }
        #endregion

        #region Methods
        public static IResponseSender Create(
            IResponseWriter responseWriter,
            IChannelWriter channelWriter,
            IDictionary<Type, string> responseTypeMapping)
        {
            var sender = new ResponseSender(
                responseWriter,
                channelWriter,
                responseTypeMapping);
            return sender;
        }

        public void Send<TResponse>(TResponse response)
            where TResponse : IResponse
        {
            var typeKey = response.GetType();
            if (!_responseTypeMapping.ContainsKey(typeKey))
            {
                throw new NotSupportedException(string.Format("Mapping of response type '{0}' is not supported.", typeKey));
            }

            using (var responseStream = new MemoryStream())
            {
                _responseWriter.Write(
                    response, 
                    responseStream);

                _channelWriter.WriteToChannel(
                    _responseTypeMapping[typeKey],
                    responseStream.GetBuffer(),
                    response.RequestId);
            }
            #endregion
        }

    }
}
