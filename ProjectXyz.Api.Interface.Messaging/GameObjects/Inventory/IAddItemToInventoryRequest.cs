using System;

namespace ProjectXyz.Api.Messaging.Interface.GameObjects.Inventory
{
    public interface IAddItemToInventoryRequest : IRequest
    {
        #region Properties
        Guid SourceGameObjectId { get; }

        Guid SourceInventoryId { get; }

        int SourceIndex { get; }

        Guid DestinationGameObjectId { get; }

        Guid DestinationInventoryId { get; }

        int DestinationIndex { get; }
        #endregion
    }
}
