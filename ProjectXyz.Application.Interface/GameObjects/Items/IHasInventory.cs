using System;
using ProjectXyz.Application.Interface.Items;

namespace ProjectXyz.Application.Interface.GameObjects.Items
{
    public interface IHasInventory : IGameObject
    {
        #region Methods
        IMutableInventory GetInventory(Guid inventoryId);
        #endregion
    }
}
