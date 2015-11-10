using System;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class EquipmentItemPath
    {
        #region Properties
        public GameObjectPath OwnerPath { get; set; }
        
        public Guid EquipmentSlotId { get; set; }
        #endregion
    }
}
