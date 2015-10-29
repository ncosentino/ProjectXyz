using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentPlugin
    {
        #region Properties
        Guid Id { get; }

        Guid EnchantmentTypeId { get; }

        string StoreRepositoryClassName { get; }

        string DefinitionRepositoryClassName { get; }
        #endregion
    }
}
