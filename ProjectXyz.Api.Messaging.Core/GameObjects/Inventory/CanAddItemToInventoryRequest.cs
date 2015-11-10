using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class CanAddItemToInventoryRequest : BaseRequest
    {
        #region Properties
        public InventoryItemPath SourceItemPath { get; set; }

        public InventoryItemPath DestinationItemPath { get; set; }
        #endregion
    }
}
