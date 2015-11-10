using ProjectXyz.Api.Messaging.Interface;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class CanEquipFromInventoryRequest : BaseRequest
    {
        #region Properties
        public InventoryItemPath SourceItemPath { get; set; }

        public EquipmentItemPath DestinationItemPath { get; set; }
        #endregion
    }
}
