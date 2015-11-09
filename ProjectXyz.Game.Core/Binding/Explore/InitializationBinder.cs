using System;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory;
using ProjectXyz.Api.Messaging.Interface.Initialization;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Core.Time;
using ProjectXyz.Application.Interface.GameObjects.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Maps;
using ProjectXyz.Application.Interface.Worlds;
using DateTime = ProjectXyz.Application.Core.Time.DateTime;

namespace ProjectXyz.Game.Core.Binding.Explore
{
    public sealed class InitializationBinder : IApiBinder
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IWorldManager _worldManager;
        private readonly IMapManager _mapManager;
        #endregion

        #region Constructors
        private InitializationBinder(
            IApiManager apiManager,
            IWorldManager worldManager,
            IMapManager mapManager)
        {
            _apiManager = apiManager;
            _worldManager = worldManager;
            _mapManager = mapManager;
            Subscribe();
        }

        ~InitializationBinder()
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
            var binder = new InitializationBinder(
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
            _apiManager.RequestRegistrar.Subscribe<IInitializeWorldRequest>(HandleInitializeWorldRequest);
        }

        private void Unsubscribe()
        {
            _apiManager.RequestRegistrar.Unsubscribe<IInitializeWorldRequest>(HandleInitializeWorldRequest);
        }

        private void HandleInitializeWorldRequest(IInitializeWorldRequest request)
        {
            var mapId = GetMapIdForPlayer(request.PlayerId);

            var map = _mapManager.GetMapById(
                mapId,
                MapContext.Create(Calendar.Create(DateTime.Create(0, 0, 0, 0, 0, 0)))); // FIXME: where do we pull this context from?
            _worldManager.World.ActivateMap(map);
        }

        private Guid GetMapIdForPlayer(Guid playerId)
        {
            // TODO: load map ID for player
            return Guid.NewGuid();
        }
        #endregion
    }
}