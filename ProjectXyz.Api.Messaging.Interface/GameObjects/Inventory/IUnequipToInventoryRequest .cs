using System;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface IUnequipToInventoryRequest : IRequest
    {
        #region Properties
        Guid SourceGameObjectId { get; }

        Guid SourceEquipmentSlotId { get; }

        Guid DestinationGameObjectId { get; }

        Guid DestinationInventoryId { get; }

        int DestinationIndex { get; }
        #endregion
    }
}
