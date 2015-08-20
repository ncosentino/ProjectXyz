using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectXyz.Data.Interface.Enchantments
{
    public interface IEnchantmentDefinition
    {
        #region Properties
        Guid Id { get; set; }
        
        Guid TriggerId { get; set; }

        Guid StatusTypeId { get; set; }

        Guid EnchantmentWeatherId { get; set; }
        #endregion
    }
}