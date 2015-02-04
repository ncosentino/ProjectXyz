using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

using ProjectXyz.Data.Interface.Enchantments;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemAffixDefinitionRepository
    {
        #region Methods
        IItemAffixDefinition GetById(Guid id);

        IEnumerable<Guid> GetIdsForFilter(int minimumLevel, int maximumLevel, Guid magicTypeId, bool prefixes, bool suffixes);
        #endregion
    }
}
