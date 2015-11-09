using System;
using System.Linq;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface.General;
using ProjectXyz.Api.Messaging.Interface.Maps;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Worlds;

namespace ProjectXyz.Game.Core.Binding
{
    public sealed class MapApiBinder : IApiBinder
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IWorldManager _worldManager;
        private readonly IMapManager _mapManager;
        #endregion

        #region Constructors
        private MapApiBinder(
            IApiManager apiManager,
            IWorldManager worldManager,
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
            IWorldManager worldManager,
            IMapManager mapManager)
        {
            var binder = new MapApiBinder(
                apiManager,
                worldManager,
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

        private void HandleMapDataRequest(IMapDataRequest request)
        {
            var activeMap = _worldManager.World.Maps.FirstOrDefault(x => x.Id == request.MapId);
            if (activeMap == null)
            {
                _apiManager.Responder.Respond<IBooleanResultResponse>(request.Id, r =>
                {
                    r.Result = false;
                    r.ErrorStringResourceId = Guid.NewGuid(); // TODO: send back a server state error? map isn't even active...
                });
            }

            // TODO: respond!
            //_apiManager.Responder.Respond();
        }
        #endregion
    }
}