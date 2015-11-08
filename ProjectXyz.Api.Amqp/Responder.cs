using System.IO;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Amqp
{
    public sealed class Responder : IResponder
    {
        #region Fields
        private readonly IResponseWriter _responseWriter;
        private readonly IChannelWriter _channelWriter;
        #endregion

        #region Constructors
        private Responder(
            IResponseWriter responseWriter,
            IChannelWriter channelWriter)
        {
            _responseWriter = responseWriter;
            _channelWriter = channelWriter;
        }
        #endregion

        #region Methods
        public static IResponder Create(
            IResponseWriter responseWriter,
            IChannelWriter channelWriter)
        {
            var responder = new Responder(
                responseWriter,
                channelWriter);
            return responder;
        }

        public void Respond<TResponse>(TResponse response)
            where TResponse : IResponse
        {
            using (var responseStream = new MemoryStream())
            {
                _responseWriter.Write(
                    response, 
                    responseStream);

                _channelWriter.WriteToChannel(
                    response.Type,
                    responseStream.GetBuffer(),
                    response.RequestId);
            }
            #endregion
        }

    }
}
