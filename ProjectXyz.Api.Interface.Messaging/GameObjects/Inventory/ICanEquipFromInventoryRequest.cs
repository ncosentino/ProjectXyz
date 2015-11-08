using System;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface ICanEquipFromInventoryRequest : IRequest
    {
        #region Properties
        Guid SourceGameObjectId { get; }

        Guid SourceInventoryId { get; }

        int SourceIndex { get; }

        Guid DestinationGameObjectId { get; }

        Guid DestinationEquipmentSlotId { get; }
        #endregion
    }
}
