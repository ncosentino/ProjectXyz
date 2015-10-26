using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Items.Contracts;

namespace ProjectXyz.Data.Interface.Items
{
    [ContractClass(typeof(IItemStoreContract))]
    public interface IItemStore : IGameObject
    {
        #region Properties
        Guid ItemDefinitionId { get; }

        Guid ItemNamePartId { get; }

        Guid InventoryGraphicResourceId { get; }

        Guid MagicTypeId { get; }

        Guid ItemTypeId { get; }

        Guid MaterialTypeId { get; }

        Guid SocketTypeId { get; }
        #endregion
    }
}