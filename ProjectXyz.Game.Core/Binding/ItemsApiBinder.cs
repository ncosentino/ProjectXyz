using System;
using ProjectXyz.Api.Interface;
using ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory;
using ProjectXyz.Api.Messaging.Interface.Maps;
using ProjectXyz.Application.Core.Maps;
using ProjectXyz.Application.Core.Time;
using ProjectXyz.Application.Interface.GameObjects.Items;
using ProjectXyz.Application.Interface.Items.ExtensionMethods;
using ProjectXyz.Application.Interface.Maps;
using DateTime = ProjectXyz.Application.Core.Time.DateTime;

namespace ProjectXyz.Game.Core.Binding
{
    public sealed class ItemsApiBinder : IApiBinder
    {
        #region Fields
        private readonly IApiManager _apiManager;
        private readonly IMapManager _mapManager;
        #endregion

        #region Constructors
        private ItemsApiBinder(
            IApiManager apiManager,
            IMapManager mapManager)
        {
            _apiManager = apiManager;
            _mapManager = mapManager;
            Subscribe();
        }

        ~ItemsApiBinder()
        {
            Dispose(false);
        }
        #endregion

        #region Methods
        public static IApiBinder Create(
            IApiManager apiManager,
            IMapManager mapManager)
        {
            var binder = new ItemsApiBinder(
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
            _apiManager.RequestRegistrar.Subscribe<ICanAddItemToInventoryRequest>(HandleCanAddItemToInventoryRequest);
        }

        private void Unsubscribe()
        {
            _apiManager.RequestRegistrar.Unsubscribe<ICanAddItemToInventoryRequest>(HandleCanAddItemToInventoryRequest);
        }

        private void HandleCanAddItemToInventoryRequest(ICanAddItemToInventoryRequest request)
        {
            // FIXME: wtf do we do about this little shit storm?
            var map = _mapManager.GetMapById(Guid.NewGuid(), MapContext.Create(Calendar.Create(DateTime.Create(1, 1, 0, 0, 0, 0))));

            var sourceObject = map.FindGameObject<IHasInventory>(request.SourceGameObjectId);
            if (sourceObject == null)
            {
                // TODO: error response
                return;
            }

            var sourceInventory = sourceObject.GetInventory(request.SourceInventoryId);
            if (sourceInventory == null)
            {
                // TODO: error response
                return;
            }

            var sourceItem = sourceInventory[request.SourceIndex];
            if (sourceItem == null)
            {
                // TODO: error response
                return;
            }

            var destinationObject = map.FindGameObject<IHasInventory>(request.DestinationGameObjectId);
            if (destinationObject == null)
            {
                // TODO: error response
                return;
            }

            var destinationInventory = sourceObject.GetInventory(request.DestinationInventoryId);
            if (destinationInventory == null)
            {
                // TODO: error response
                return;
            }

            var canAddItem = destinationInventory.CanAddItem(
                request.DestinationIndex, 
                sourceItem);

            // TODO: respond...
        }
        #endregion
    }
}