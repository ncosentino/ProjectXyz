using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Core
{
    public sealed class ApiManager : IApiManager
    {
        #region Constructors
        private ApiManager(
            IRequestRegistrar requestRegistrar,
            INotifier notifier,
            IResponder responder)
        {
            RequestRegistrar = requestRegistrar;
            Notifier = notifier;
            Responder = responder;
        }
        #endregion

        #region Properties
        /// <inheritdoc/>
        public INotifier Notifier { get; }

        /// <inheritdoc/>
        public IResponder Responder { get; }
        
        /// <inheritdoc/>
        public IRequestRegistrar RequestRegistrar { get; }

        /// <inheritdoc/>
        public IResponseFactory ResponseFactory { get; }
        #endregion

        #region Methods
        public static IApiManager Create(
            IRequestRegistrar requestRegistrar,
            INotifier notifier,
            IResponder responder)
        {
            var manager = new ApiManager(
                requestRegistrar,
                notifier,
                responder);
            return manager;
        }
        #endregion
    }
}
