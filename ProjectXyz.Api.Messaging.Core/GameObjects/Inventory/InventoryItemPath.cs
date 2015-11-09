using System;
using ProjectXyz.Api.Messaging.Interface.GameObjects;
using ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory;

namespace ProjectXyz.Api.Messaging.Core.GameObjects.Inventory
{
    public sealed class InventoryItemPath : IInventoryItemPath
    {
        #region Properties
        public IGameObjectPath OwnerPath { get; set; }

        public Guid InventoryId { get; set; }

        public int Index { get; set; }
        #endregion
    }
}
