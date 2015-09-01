using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionMagicTypeGroupingFactory
    {
        #region Methods
        IItemAffixDefinitionMagicTypeGrouping Create(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypesGroupingId);
        #endregion
    }
}
