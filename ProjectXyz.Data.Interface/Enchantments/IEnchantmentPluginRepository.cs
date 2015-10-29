using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentPluginRepository
    {
        #region Methods
        IEnchantmentPlugin Add(
            Guid id,
            Guid enchantmentTypeId,
            string storeRepositoryClassName,
            string definitionRepositoryClassName);

        IEnchantmentPlugin GetById(Guid id);

        IEnchantmentPlugin GetByStoreRepositoryClassName(string className);

        IEnchantmentPlugin GetByDefinitionRepositoryClassName(string className);

        IEnchantmentPlugin GetByEnchantmentTypeId(Guid enchantmentTypeId);

        IEnumerable<IEnchantmentPlugin> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
