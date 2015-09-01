using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionMagicTypeGroupinRepository
    {
        #region Methods
        IItemAffixDefinitionMagicTypeGrouping Add(
            Guid id,
            Guid itemAffixDefinitionId,
            Guid magicTypesGroupingId);

        IItemAffixDefinitionMagicTypeGrouping GetById(Guid id);

        IItemAffixDefinitionMagicTypeGrouping GetByItemAffixDefinitionId(Guid itemAffixDefinitionId);

        void RemoveById(Guid id);

        IEnumerable<IItemAffixDefinitionMagicTypeGrouping> GetAll();
        #endregion
    }
}
