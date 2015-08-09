using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentTypeDefinitionRepository<TEnchantmentDefinition>
        where TEnchantmentDefinition : IAdditiveEnchantmentDefinition
    {
        #region Methods
        TEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
