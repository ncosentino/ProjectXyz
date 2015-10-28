using System;
using System.Collections.Generic;

namespace ProjectXyz.Data.Interface.Items.Names
{
    public interface IItemNameAffixFilter
    {
        #region Methods

        IItemNameAffix GetRandom(Guid itemTypeId, Guid magicTypeId, bool prefix);

        IEnumerable<IItemNameAffix> Filter(Guid itemTypeId, Guid magicTypeId);
        #endregion
    }
}
