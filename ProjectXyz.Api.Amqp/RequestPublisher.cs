using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Interface.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProjectXyz.Api.Amqp
{
    public sealed class RequestPublisher : IRequestPublisher
    {
        #region Fields
        private readonly BlockingCollection<BasicDeliverEventArgs> _queue;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly EventingBasicConsumer _consumer;
        private readonly Task[] _consumerTasks;
        private readonly IRequestFactory _requestFactory;
        #endregion

        #region Constructors
        private RequestPublisher(
            EventingBasicConsumer consumer,
            IRequestFactory requestFactory,
            int numberOfThreads, 
            int backlogSize)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _consumerTasks = new Task[numberOfThreads];
            _queue = new BlockingCollection<BasicDeliverEventArgs>(backlogSize);

            _requestFactory = requestFactory;
            _consumer = consumer;
            _consumer.Received += Consumer_Received;

            for (int i = 0; i < numberOfThreads; ++i)
            {
                var task = Task.Factory.StartNew(
                   () => Consume(_queue, _cancellationTokenSource.Token),
                   _cancellationTokenSource.Token);
                _consumerTasks[i] = task;
            }
        }

        ~RequestPublisher()
        {
            Dispose(false);
        }
        #endregion

        #region Events
        public event EventHandler<RequestPublishedEventArgs> Publish;
        #endregion

        #region Methods
        public static IRequestPublisher Create(
            EventingBasicConsumer consumer,
            IRequestFactory requestFactory,
            int numberOfThreads,
            int backlogSize)
        {
            var publisher = new RequestPublisher(
                consumer,
                requestFactory,
                numberOfThreads,
                backlogSize);
            return publisher;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _consumer.Received -= Consumer_Received;
            _cancellationTokenSource.Cancel();
            Task.WaitAll(_consumerTasks.ToArray());
        }

        private void OnPublish(IRequest request)
        {
            Publish?.Invoke(this, new RequestPublishedEventArgs(request));
        }

        private void Consume(BlockingCollection<BasicDeliverEventArgs> queue, CancellationToken token)
        {
            foreach (var basicDeliverEventArgs in queue.GetConsumingEnumerable())
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                var request = _requestFactory.Create(basicDeliverEventArgs);
                OnPublish(request);
            }
        }
        #endregion

        #region Event Handlers
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            _queue.Add(e);
        }
        #endregion
    }
}
