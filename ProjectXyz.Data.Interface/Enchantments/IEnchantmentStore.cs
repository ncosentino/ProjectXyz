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

        TimeSpan RemainingDuration { get; }
        #endregion
    }

    public interface IAdditiveEnchantmentStore : IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }

    public interface IPercentageEnchantmentStore : IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }

        double Value { get; }
        #endregion
    }

    public interface IOneShotNegateEnchantmentStore : IEnchantmentStore
    {
        #region Properties
        Guid StatId { get; }
        #endregion
    }
}
