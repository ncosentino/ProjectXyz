using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Interface.Messaging;

namespace ProjectXyz.Api.Core
{
    public sealed class RequestRegistrar : IRequestRegistrar
    {
        #region Fields
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<int, Action<object>>> _handlers;
        private readonly IRequestPublisher _publisher;
        #endregion

        #region Constructors
        private RequestRegistrar(IRequestPublisher publisher)
        {
            _handlers = new ConcurrentDictionary<Type, ConcurrentDictionary<int, Action<object>>>();
            _publisher = publisher;
            _publisher.Publish += Publisher_Publish;
        }

        ~RequestRegistrar()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IRequestRegistrar Create(IRequestPublisher publisher)
        {
            var registrar = new RequestRegistrar(publisher);
            return registrar;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Subscribe<TRequest>(Action<TRequest> handler) 
            where TRequest : IRequest
        {
            var key = typeof(TRequest);
            if (!_handlers.ContainsKey(key))
            {
                _handlers[key] = new ConcurrentDictionary<int, Action<object>>();
            }

            _handlers[key][handler.GetHashCode()] = x => handler.Invoke((TRequest)x);
        }

        public void Unsubscribe<TRequest>(Action<TRequest> handler) 
            where TRequest : IRequest
        {
            var key = typeof(TRequest);
            if (!_handlers.ContainsKey(key))
            {
                return;
            }

            Action<object> removedHandler;
            _handlers[key].TryRemove(handler.GetHashCode(), out removedHandler);

            if (_handlers[key].Count < 1)
            {
                ConcurrentDictionary<int, Action<object>> removedHandlerMapping;
                _handlers.TryRemove(key, out removedHandlerMapping);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            _publisher.Publish -= Publisher_Publish;
        }
        #endregion

        #region Event Handlers
        private void Publisher_Publish(object sender, RequestPublishedEventArgs e)
        {
            var key = e.Request.GetType();
            if (!_handlers.ContainsKey(key))
            {
                return;
            }

            foreach (var handler in _handlers[key].Values)
            {
                handler.Invoke(e.Request);
            }
        }
        #endregion
    }
}
