using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using ProjectXyz.Application.Interface.Items.Contracts;

namespace ProjectXyz.Application.Interface.Items
{
    [ContractClass(typeof(IItemMetaDataContract))]
    public interface IItemMetaData
    {
        #region Properties

        Guid InventoryGraphicResourceId { get; }

        Guid MagicTypeId { get; }

        Guid ItemTypeId { get; }

        Guid MaterialTypeId { get; }

        Guid SocketTypeId { get; }
        #endregion
    }
}
