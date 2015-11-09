using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface IInventoryItemPath
    {
        #region Properties
        IGameObjectPath OwnerPath { get; }
        
        Guid InventoryId { get; }

        int Index { get; }
        #endregion
    }
}
