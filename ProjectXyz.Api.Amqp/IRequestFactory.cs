using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectXyz.Api.Messaging.Interface;
using RabbitMQ.Client.Events;

namespace ProjectXyz.Api.Amqp
{
    public interface IRequestFactory
    {
        #region Methods
        IRequest Create(BasicDeliverEventArgs deliverEventArgs);
        #endregion
    }
}
