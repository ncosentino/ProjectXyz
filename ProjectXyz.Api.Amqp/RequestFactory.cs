using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface.Messaging;
using RabbitMQ.Client.Events;

namespace ProjectXyz.Api.Amqp
{
    public sealed class RequestFactory : IRequestFactory
    {
        #region Constructors
        private RequestFactory()
        {
        }
        #endregion

        #region Methods
        public static IRequestFactory Create()
        {
            var factory = new RequestFactory();
            return factory;
        }

        public IRequest Create(BasicDeliverEventArgs deliverEventArgs)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
