using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items.Affixes
{
    public interface IItemAffixDefinitionRepository
    {
        #region Methods
        IItemAffixDefinition GetById(Guid id);

        IEnumerable<IItemAffixDefinition> GetAll();

        IItemAffixDefinition Add(
            Guid id,
            Guid nameStringResourceId,
            bool isPrefix,
            int minimumLevel,
            int maximumLevel);

        void RemoveById(Guid id);
        #endregion
    }
}
