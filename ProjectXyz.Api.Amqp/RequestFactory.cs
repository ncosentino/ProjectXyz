using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, Type> _requestMapping;
        private readonly IRequestReader _requestReader;
        #endregion

        #region Constructors
        private RequestFactory(
            IRequestReader requestReader,
            IDictionary<string, Type> requestMapping)
        {
            _requestReader = requestReader;
            _requestMapping = new Dictionary<string, Type>(requestMapping);
        }
        #endregion

        #region Methods
        public static IRequestFactory Create(
            IRequestReader requestReader,
            IDictionary<string, Type> requestMapping)
        {
            var factory = new RequestFactory(
                requestReader,
                requestMapping);
            return factory;
        }

        public IRequest Create(BasicDeliverEventArgs deliverEventArgs)
        {
            if (!deliverEventArgs.BasicProperties.Headers.ContainsKey("Type"))
            {
                throw new InvalidOperationException("The delivered message does not contain a 'Type' header.");
            }

            var typeName = Convert.ToString(
                Encoding.UTF8.GetString(deliverEventArgs.BasicProperties.Headers["Type"] as byte[]),
                CultureInfo.InvariantCulture);

            if (!_requestMapping.ContainsKey(typeName))
            {
                throw new NotSupportedException(string.Format("Mapping of request type '{0}' is not supported.", typeName));
            }

            IRequest request;
            using (var bodyStream = new MemoryStream(deliverEventArgs.Body))
            {
                request = _requestReader.Read(
                    bodyStream, 
                    _requestMapping[typeName]);
            }

            return request;
        }
        #endregion
    }
}
