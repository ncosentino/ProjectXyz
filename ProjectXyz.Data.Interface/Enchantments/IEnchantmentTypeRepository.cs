using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentTypeRepository
    {
        #region Methods
        IEnchantmentType Add(
            Guid id,
            string storeRepositoryClassName,
            string definitionRepositoryClassName);

        IEnchantmentType GetById(Guid id);

        IEnchantmentType GetByEnchantmentDefinitionId(Guid enchantmentDefinitionId);

        IEnumerable<IEnchantmentType> GetAll();

        void RemoveById(Guid id);
        #endregion
    }
}
