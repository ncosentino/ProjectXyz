using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface IEquipmentItemPath
    {
        #region Properties
        IGameObjectPath OwnerPath { get; }
        
        Guid EquipmentSlotId { get; }
        #endregion
    }
}
