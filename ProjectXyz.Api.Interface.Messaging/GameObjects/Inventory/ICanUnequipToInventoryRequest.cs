using System;

namespace ProjectXyz.Api.Interface.Messaging.GameObjects.Inventory
{
    public interface ICanUnequipToInventoryRequest : IRequest
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
