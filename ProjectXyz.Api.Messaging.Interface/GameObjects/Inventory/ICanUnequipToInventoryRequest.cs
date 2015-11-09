using System;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface ICanUnequipToInventoryRequest : IRequest
    {
        #region Properties
        IEquipmentItemPath SourceItemPath { get; }

        IInventoryItemPath DestinationItemPath { get; }
        #endregion
    }
}
