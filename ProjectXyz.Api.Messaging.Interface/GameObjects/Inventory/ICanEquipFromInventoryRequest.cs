using System;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface ICanEquipFromInventoryRequest : IRequest
    {
        #region Properties
        IInventoryItemPath SourceItemPath { get; }

        IEquipmentItemPath DestinationItemPath { get; }
        #endregion
    }
}
