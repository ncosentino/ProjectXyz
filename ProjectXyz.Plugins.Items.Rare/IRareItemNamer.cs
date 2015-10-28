using System;
using System.Collections.Generic;
using ProjectXyz.Data.Interface.Items.Names;

namespace ProjectXyz.Plugins.Items.Rare
{
    public interface IRareItemNamer
    {
        IEnumerable<IItemNamePart> CreateItemName(
            Guid itemTypeId,
            Guid magicTypeId);
    }
}