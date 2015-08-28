using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionFilterRepository
    {
        #region Methods
        IEnumerable<Guid> GetIdsForFilter(int minimumLevel, int maximumLevel, Guid magicTypeId, bool prefixes, bool suffixes);
        #endregion
    }
}
