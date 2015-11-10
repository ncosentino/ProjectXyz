using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class UnequipToInventoryRequest : BaseRequest
    {
        #region Properties
        public EquipmentItemPath SourceItemPath { get; set; }

        public InventoryItemPath DestinationItemPath { get; set; }
        #endregion
    }
}
