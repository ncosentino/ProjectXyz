using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class AddItemToInventoryRequest : BaseRequest
    {
        #region Properties
        public InventoryItemPath SourceItemPath { get; set; }

        public InventoryItemPath DestinationItemPath { get; set; }
        #endregion
    }
}
