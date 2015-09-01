using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using ProjectXyz.Data.Interface.Enchantments.Contracts;

namespace ProjectXyz.Data.Interface.Enchantments
{
    [ContractClass(typeof(IEnchantmentStoreContract))]
    public interface IEnchantmentStore
    {
        #region Properties
        Guid Id { get; }

        Guid TriggerId { get; }

        Guid StatusTypeId { get; }

        Guid EnchantmentTypeId { get; }

        Guid WeatherGroupingId { get; }
        #endregion
    }
}
