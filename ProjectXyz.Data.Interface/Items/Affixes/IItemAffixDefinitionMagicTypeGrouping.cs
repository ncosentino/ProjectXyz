using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionMagicTypeGrouping
    {
        #region Properties
        Guid Id { get; }

        Guid ItemAffixDefinitionId { get; }

        Guid MagicTypeGroupingId { get; }
        #endregion
    }
}
