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

        void Add(IItemAffixDefinition itemStore);

        void RemoveById(Guid id);
        #endregion
    }
}
