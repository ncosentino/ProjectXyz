using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Items
{
    public interface IItemTypeGeneratorPluginRepository
    {
        #region Methods
        IItemTypeGeneratorPlugin Add(
            Guid id,
            Guid magicTypeId,
            string itemGeneratorClassName);

        void RemoveById(Guid id);

        IItemTypeGeneratorPlugin GetById(Guid id);

        IItemTypeGeneratorPlugin GetByMagicTypeId(Guid magicTypeId);

        IItemTypeGeneratorPlugin GetByItemGeneratorClassName(string itemGeneratorClassName);

        IEnumerable<IItemTypeGeneratorPlugin> GetAll();
        #endregion
    }
}
