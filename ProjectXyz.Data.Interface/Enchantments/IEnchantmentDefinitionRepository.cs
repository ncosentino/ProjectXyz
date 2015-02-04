using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinitionRepository
    {
        #region Methods
        IEnchantmentDefinition GetById(Guid id);
        #endregion
    }
}
