using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentType
    {
        #region Properties
        Guid Id { get; }

        string StoreRepositoryClassName { get; }

        string DefinitionRepositoryClassName { get; }
        #endregion
    }
}
