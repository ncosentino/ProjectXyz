using System;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface.Maps;
using ProjectXyz.Application.Interface.Maps;

namespace ProjectXyz.Game.Core.Binding
{
    public sealed class MapApiBinder : IApiBinder
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IMapManager _mapManager;
        #endregion

        #region Constructors
        private MapApiBinder(
            IApiManager apiManager,
            IMapManager mapManager)
        {
            _apiManager = apiManager;
            _mapManager = mapManager;
            Subscribe();
        }

        ~MapApiBinder()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IApiBinder Create(
            IApiManager apiManager,
            IMapManager mapManager)
        {
            var binder = new MapApiBinder(
                apiManager,
                mapManager);
            return binder;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            Unsubscribe();
        }

        private void Subscribe()
        {
            _apiManager.RequestRegistrar.Subscribe<IMapDataRequest>(HandleMapDataRequest);
        }

        private void Unsubscribe()
        {
            _apiManager.RequestRegistrar.Unsubscribe<IMapDataRequest>(HandleMapDataRequest);
        }

        private void HandleMapDataRequest(IMapDataRequest mapDataRequest)
        {
            // TODO: respond!
            //_apiManager.Responder.Respond();
        }
        #endregion
    }
}