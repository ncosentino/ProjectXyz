using System;
using System.Globalization;
using System.IO;
using System.Text;
using ProjectXyz.Api.Messaging.Interface;
using RabbitMQ.Client.Events;

namespace ProjectXyz.Api.Amqp
{
    public sealed class RequestFactory : IRequestFactory
    {
        #region Fields
        private readonly IRequestReader _requestReader;
        #endregion

        #region Constructors
        private RequestFactory(IRequestReader requestReader)
        {
            _requestReader = requestReader;
        }
        #endregion

        #region Methods
        public static IRequestFactory Create(IRequestReader requestReader)
        {
            var factory = new RequestFactory(requestReader);
            return factory;
        }

        public IRequest Create(BasicDeliverEventArgs deliverEventArgs)
        {
            if (!deliverEventArgs.BasicProperties.Headers.ContainsKey("Type"))
            {
                throw new InvalidOperationException("The delivered message does not contain a 'Type' header.");
            }

            var type = Convert.ToString(
                Encoding.UTF8.GetString(deliverEventArgs.BasicProperties.Headers["Type"] as byte[]),
                CultureInfo.InvariantCulture);

            IRequest request;
            using (var bodyStream = new MemoryStream(deliverEventArgs.Body))
            {
                // TODO: map the type to a... type.
                request = null;
                ////request = _requestReader.Read(bodyStream, typeof(BooleanResultResponse));
            }

            return request;
        }
        #endregion
    }
}
