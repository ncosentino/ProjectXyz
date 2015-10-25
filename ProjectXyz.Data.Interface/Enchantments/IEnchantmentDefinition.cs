using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; }

        Guid EnchantmentTypeId { get; }
        
        Guid TriggerId { get; }

        Guid StatusTypeId { get; }
        #endregion
    }
}